using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AdminCompras : MonoBehaviour
{
    public GameObject PanelData;

    public Image Base;



    public Sprite[] MotorImagenes;
    public Sprite[] PistonImagenes;
    public Sprite[] NitroImagenes;
    public Sprite[] turbosImagenes;


    public bool Motor;
    public bool Piston;
    public bool Nitro;
    public bool Turbo;

    public bool trafa;

    public TurboTienda TB;
    public NitroTienda NI;
    public PistonTienda PI;
    public EngineTienda EN;

    public bool kla = false;


    public Text titulo;
    public Text HP;
    public Text Peso;
    public Text Estado;
    public Text Precio;
    public Text guita;

    public Button CE;
    public Button CP;
    public Button CN;
    public Button CT;
    public bool panel;

    public GameObject ET;
    public GameObject PT;
    public GameObject NT;
    public GameObject TT;







    public int Index;
    void Start()
    {
        PanelData.SetActive(false);
        ET.SetActive(false);
        PT.SetActive(false);
        NT.SetActive(false);
        TT.SetActive(false);
        //PlayerPrefs.SetInt("currency", 1900000000);
        //PlayerPrefs.SetInt("currency", 1900000000);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (TB.jijijija == true || NI.jijijija == true || PI.jijijija == true || EN.jijijija == true)
        {
            kla = true;
        }

        else
        {
            kla = false;
        }

        if (kla == false)
        {
            guita.text = "$" + PlayerPrefs.GetInt("currency").ToString();

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cerrarPanel();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && trafa == true)
        {
            Der();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && trafa == true)
        {
            Izq();
        }

        

        if (Motor == true)
        {
            CE.gameObject.SetActive(true);
            CP.gameObject.SetActive(false);
            CN.gameObject.SetActive(false);
            CT.gameObject.SetActive(false);

            Base.sprite = MotorImagenes[Index];
            titulo.text = EN.nom[Index];
            string jk = EN.HPs[Index].ToString();
            HP.text = "HP: " + jk;

            string lm = EN.Pesosim[Index].ToString();
            Peso.text = "Peso: " + lm;

            if (PlayerPrefs.HasKey(EN.engineKeys[Index]))
            {
                Estado.text = "Status: Owned"; // Mostrar que el motor es propiedad
            }
            else
            {
                Estado.text = "Status: Not Owned"; // Mostrar que el motor no es propiedad
            }

            titulo.color = EN.colors[Index];

            string agr = EN.Precio[Index].ToString();
            Precio.text = "$" + agr;

            if (EN.jijijija == false)
            {
                string ghj = PlayerPrefs.GetInt("currency").ToString();
                guita.text = "$" + ghj;
                guita.color = Color.white;
            }

        }
        else if (Piston == true)
        {
            CE.gameObject.SetActive(false);
            CP.gameObject.SetActive(true);
            CN.gameObject.SetActive(false);
            CT.gameObject.SetActive(false);

            Base.sprite = PistonImagenes[Index];
            titulo.text = PI.nom[Index];
            string jk = PI.HPs[Index].ToString();
            HP.text = "HP: " + jk;

            string lm = PI.Pesosim[Index].ToString();
            Peso.text = "Peso: " + lm;

            if (PlayerPrefs.HasKey(PI.pistonKeys[Index]))
            {
                Estado.text = "Status: Owned"; // Mostrar que el motor es propiedad
            }
            else
            {
                Estado.text = "Status: Not Owned"; // Mostrar que el motor no es propiedad
            }

            titulo.color = PI.colors[Index];

            string agr = PI.Precio[Index].ToString();
            Precio.text = "$" + agr;

            if (PI.jijijija == false)
            {
                string ghj = PlayerPrefs.GetInt("currency").ToString();
                guita.text = "$" + ghj;
                guita.color = Color.white;
            }
        }

        else if (Nitro == true)
        {
            CE.gameObject.SetActive(false);
            CP.gameObject.SetActive(false);
            CN.gameObject.SetActive(true);
            CT.gameObject.SetActive(false);

            Base.sprite = NitroImagenes[Index];
            titulo.text = NI.nom[Index];
            string jk = NI.HPs[Index].ToString();
            HP.text = "HP: " + jk;

            string lm = NI.Pesosim[Index].ToString();
            Peso.text = "Peso: " + lm;

            if (PlayerPrefs.HasKey(NI.nitroKeys[Index]))
            {
                Estado.text = "Status: Owned"; // Mostrar que el motor es propiedad
            }
            else
            {
                Estado.text = "Status: Not Owned"; // Mostrar que el motor no es propiedad
            }

            titulo.color = NI.colors[Index];

            string agr = NI.Precio[Index].ToString();
            Precio.text = "$" + agr;

            if (NI.jijijija == false)
            {
                string ghj = PlayerPrefs.GetInt("currency").ToString();
                guita.text = "$" + ghj;
                guita.color = Color.white;
            }
        }

        else if (Turbo == true)
        {
            CE.gameObject.SetActive(false);
            CP.gameObject.SetActive(false);
            CN.gameObject.SetActive(false);
            CT.gameObject.SetActive(true);

            Base.sprite = turbosImagenes[Index];
            titulo.text = TB.nom[Index];
            string jk = TB.PSIs[Index].ToString();
            HP.text = "PSI: " + jk;

            string lm = TB.Pesosim[Index].ToString();
            Peso.text = "Peso: " + lm;

            if (PlayerPrefs.HasKey(TB.turboKeys[Index]))
            {
                Estado.text = "Status: Owned"; // Mostrar que el motor es propiedad
            }
            else
            {
                Estado.text = "Status: Not Owned"; // Mostrar que el motor no es propiedad
            }

            titulo.color = TB.colors[Index];

            string agr = TB.Precio[Index].ToString();
            Precio.text = "$" + agr;

            if (TB.jijijija == false)
            {
                string ghj = PlayerPrefs.GetInt("currency").ToString();
                guita.text = "$" + ghj;
                guita.color = Color.white;
            }
        }

        

        





    }

    public void EngineKaput()
    {
        if (panel == false)
        {
            PanelData.SetActive(true);
            Motor = true;
            trafa = true;
            panel = true;
            ET.SetActive(true); 
        }

    }

    public void PistonGordo()
    {
        if (panel == false)
        {
            PanelData.SetActive(true);
            Piston = true;
            trafa = true;
            panel = true; 
            PT.SetActive(true);
           
        }

    }

    public void NitroLargo()
    {
        if (panel == false)
        {
            PanelData.SetActive(true);
            Nitro = true;
            trafa = true;
            panel = true;
            NT.SetActive(true);
            
        }

    }

    public void turboManuela()
    {
        if (panel == false)
        {
            PanelData.SetActive(true);
            Turbo = true;
            trafa = true;
            panel = true;
            TT.SetActive(true);
        }
        
    }

    public void cerrarPanel()
    {
        PanelData.SetActive(false);
        Index = 0;
        trafa = false;
        panel = false;
        Motor = false;
        Piston = false;
        Nitro = false;
        Turbo = false;
        ET.SetActive(false);
        PT.SetActive(false);
        NT.SetActive(false);
        TT.SetActive(false);
    }



    public void vueltovich()
    {
        SceneManager.LoadScene("PlataformaSelectiva");
    }

    public void Der()
    {
        if (Motor == true)
        {
            if (Index < MotorImagenes.Length - 1)
            {
                Index = Index + 1;
            }
        }
        else if (Piston == true)
        {
            if (Index < PistonImagenes.Length - 1)
            {
                Index = Index + 1;
            }
        }

        else if (Nitro == true)
        {
            if (Index < NitroImagenes.Length - 1)
            {
                Index = Index + 1;
            }
        }

        else if (Turbo == true)
        {
            if (Index < turbosImagenes.Length - 1)
            {
                Index = Index + 1;
            }
        }


     
    }

    public void Izq()
    {
        if (Index > 0)
        {
            Index = Index - 1;
        }

    }


}
