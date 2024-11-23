using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Misiones : MonoBehaviour
{
    public bool jkiu;
    public float time;
    public float startTime;
    public Text txtTiempo;
    public GameObject resultados;
    bool termino = false;
    public Text txtTempo;
    public Text txtMejorTiempo;
    public Text txtEstado;
    public float Mejor = 2f;
    public bool Ganaste;
    public Collision Colu;
    public bool nein;
    public ControlPosta RR;
    [SerializeField] private Mision misionsita;
    public GameObject Meta;
    public GameObject Spawner;
    public ObjectSpawner NT;
    public GameObject resulatadosSpawneo;
    public Text cantidadObjeto;
    public ObjectSpawner LA;
    public CollisionObjeto GH;
    public Text Premio;
    public GameObject newMejor;
    public bool iop;
    public Text guita;
    public bool jkl;

    public int gjh;
    public int uiy;


    internal enum Mision
    {
        ContraTiempo,
        RecogerCosas
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetString("Modo De Juego") == "Contra Reloj")
        {
            misionsita = Mision.ContraTiempo;

            if (misionsita == Mision.ContraTiempo)
            {
                time = startTime;
                resultados.SetActive(false);
                Meta.SetActive(true);
                txtTiempo.gameObject.SetActive(true);

            }
        }


        if (PlayerPrefs.GetString("Modo De Juego") == "Recoger Cosas")
        {
            misionsita = Mision.RecogerCosas;

            if (misionsita == Mision.RecogerCosas)
            {
                LA.enabled = true;
                GH.enabled = true;

                resulatadosSpawneo.SetActive(false);
                Spawner.SetActive(true);
                cantidadObjeto.gameObject.SetActive(true);

            }
        }

            

        else if (misionsita != Mision.RecogerCosas)
        {
            LA.enabled = false;
            GH.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        

       
        if (RR.CalentonJ == true)
        {
            if (misionsita == Mision.ContraTiempo)
            {
                if (Colu.ColuTermi == true && nein == false)
                {
                    Ganaste = true;
                }

                if (termino == false)
                {
                    time -= Time.deltaTime;

                }
                //cambia de color cuando llega a cero
                if (time < 4)
                {
                    txtTiempo.color = Color.red;
                }

                else
                {
                    txtTiempo.color = Color.white;

                }
                txtTiempo.text = time.ToString();

                if (time <= 0 || Colu.ColuTermi == true)
                {
                    termino = true;
                    resultados.SetActive(true);

                    if (Ganaste == false)
                    {
                        time = 0;
                        txtEstado.text = "Perdiste";
                        txtEstado.color = Color.red;
                        nein = true;
                    }

                    else if (Ganaste == true && nein == false)
                    {
                        txtEstado.text = "Ganaste";
                        txtEstado.color = Color.green;
                        Mejor = PlayerPrefs.GetFloat("MT");

                        if (Mathf.Abs(time) > Mejor)
                        {
                            Mejor = time;
                            RR.Record = Mejor;
                            PlayerPrefs.SetFloat("MT", time);
                            newMejor.SetActive(true);
                            Debug.Log("Es mayor");
                            iop = true;
                        }

                        else
                        {
                            if (iop == false)
                            {
                                newMejor.SetActive(false);
                                Debug.Log("Es menor");
                            }
                            
                        }


                    }

                    txtTempo.text = time.ToString();
                    txtMejorTiempo.text = Mejor.ToString();
                    float jpg = 131.78f * time + 3868.22f;
                    if (time == 0)
                    {
                        jpg = 0;
                    }
                    Premio.text = "$" + Mathf.Round(jpg).ToString();
                    int uiy = PlayerPrefs.GetInt("currency");

                    if (jkiu == false)
                    {
                        PlayerPrefs.SetInt("currency", uiy + (int)Mathf.Round(jpg));
                        jkiu = true;
                    }




                }
            }
        }
       

        else if (misionsita != Mision.ContraTiempo)
        {
            resultados.SetActive(false);
            txtTiempo.gameObject.SetActive(false);
            Meta.SetActive(false);
        }

        if (misionsita == Mision.RecogerCosas)
        {
            if (NT.VoyPaLla == NT.cantidadDeMonedas - 4)
            {
                resulatadosSpawneo.SetActive(true);
                cantidadObjeto.color = Color.green;
               

                if (jkl == false)
                {
                    gjh = Random.Range(2000, 11001);
                    uiy = PlayerPrefs.GetInt("currency");
                    jkl = true;

                }


                if (jkiu == false)
                {
                    PlayerPrefs.SetInt("currency", uiy + gjh);
                    jkiu = true;
                }

                guita.text = "$" + gjh.ToString();


            }
        }

        else if (misionsita != Mision.RecogerCosas)
        {
            resulatadosSpawneo.SetActive(false);
            cantidadObjeto.gameObject.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Volver();
        }
    }

    public void Volver()
    {
        SceneManager.LoadScene("PlataformaSelectiva");
    }
}
