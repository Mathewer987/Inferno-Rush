using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager Instance;
    public string carIndex;
    public Material mat;
    public Renderer renderer;
    public GameObject autoPadre; // El objeto vacío que contiene el auto instanciado
    public GameObject autinho;
    public GameObject CCR;
    public GameObject final;
    public PanelDataPinturaPosta PP;
    public string nombreEscenaObjetivo = "Prueba Manejo"; // Nombre de la escena objetivo


    // Start is called before the first frame update
    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);


    }
    void Start()
    {
        PP = FindObjectOfType<PanelDataPinturaPosta>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GameObject miObjeto = GameObject.Find("Autos");

        autoPadre = miObjeto;

        if (SceneManager.GetActiveScene().name == "Prueba Manejo")
        {

            for (int i = 0; i < autoPadre.transform.childCount; i++)
            {
                GameObject child = autoPadre.transform.GetChild(i).gameObject;

                if (child.activeInHierarchy)
                {
                    // Haz algo con el hijo activo, por ejemplo:
                    autinho = child;
                    break;
                }
            }

        CCR = autinho.transform.Find("Camaro con ruedas").gameObject;
        final = CCR.transform.Find("CUERPO").gameObject;
        renderer = final.GetComponent<Renderer>();
        renderer.material = mat;


        }
    }
}
