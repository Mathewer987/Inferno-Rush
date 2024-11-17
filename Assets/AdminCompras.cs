using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AdminCompras : MonoBehaviour
{
    public GameObject PanelData;

    public Image Base;


    public TurboTienda PeTe;

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







    public int Index;
    void Start()
    {
        PanelData.SetActive(false);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
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


        }
        else if (Piston == true)
        {
            CE.gameObject.SetActive(false);
            CP.gameObject.SetActive(true);
            CN.gameObject.SetActive(false);
            CT.gameObject.SetActive(false);
        }

        else if (Nitro == true)
        {
            CE.gameObject.SetActive(false);
            CP.gameObject.SetActive(false);
            CN.gameObject.SetActive(true);
            CT.gameObject.SetActive(false);
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

        }

        if (TB.jijijija == false)
        {
            string ghj = PlayerPrefs.GetInt("currency").ToString();
            guita.text = "$" + ghj;
            guita.color = Color.white;
        }
        
    }

    public void turboManuela()
    {
        PanelData.SetActive(true);
        Turbo = true;
        trafa = true;
    }

    public void cerrarPanel()
    {
        PanelData.SetActive(false);
        Index = 0;
        trafa = false;

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
                Debug.Log("Der turbo");
            }
        }

        Debug.Log("Der norm");

     
    }

    public void Izq()
    {
        if (Index > 0)
        {
            Index = Index - 1;
        }

    }


}
