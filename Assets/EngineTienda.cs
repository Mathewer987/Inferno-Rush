using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTienda : MonoBehaviour
{
    public string[] nom;
    public int[] HPs;
    public int[] Pesosim;
    public List<Color> colors;

    public int[] Precio;

    public bool jijijija;

    public string[] engineKeys;

    public AdminCompras ACV;


    void Start()
    {
        //PlayerPrefs.SetInt("currency", 10000000);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyEngine(int engineIndex)
    {

        engineIndex = ACV.Index;

        if (PlayerPrefs.GetInt("currency") >= Precio[engineIndex])
        {

            if (PlayerPrefs.GetString(engineKeys[engineIndex]) != "owned")
            {
                // Aquí podrías agregar la lógica para verificar monedas o recursos
                // Por simplicidad, asumimos que siempre se puede comprar
                PlayerPrefs.SetString(engineKeys[engineIndex], "owned");

                Debug.Log("Engine " + engineKeys[engineIndex] + " ahora es tuyo!");

                int jhg = PlayerPrefs.GetInt("currency");

                PlayerPrefs.SetInt("currency", jhg - Precio[engineIndex]);
            }
            else
            {
                Debug.Log("Ya posees este engine!");
            }
        }

        else
        {
            ACV.guita.text = "Medio cortina master";
            ACV.guita.color = Color.red;
            jijijija = true;
            StartCoroutine(FAFA());
        }
    }

    private IEnumerator FAFA()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            jijijija = false;

        }


    }
}
