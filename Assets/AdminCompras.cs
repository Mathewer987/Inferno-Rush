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

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && trafa == true)
        {
            compra();
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

    public void compra()
    {

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
