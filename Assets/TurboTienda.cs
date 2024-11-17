using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboTienda : MonoBehaviour
{
    public string[] nom;
    public int[] PSIs;
    public int[] Pesosim;
    public List<Color> colors;

    public int[] Precio;

    public bool jijijija;

    public string[] turboKeys;

    public AdminCompras ACV;


    void Start()
    {
        //PlayerPrefs.SetInt("currency", 1000000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTurbo(int turboIndex)
    {

        turboIndex = ACV.Index;
        
      if (PlayerPrefs.GetInt("currency") >= Precio[turboIndex])
        {

        if (PlayerPrefs.GetString(turboKeys[turboIndex]) != "owned")
        {
            // Aquí podrías agregar la lógica para verificar monedas o recursos
            // Por simplicidad, asumimos que siempre se puede comprar
            PlayerPrefs.SetString(turboKeys[turboIndex], "owned");

            Debug.Log("Turbo " + turboKeys[turboIndex] + " ahora es tuyo!");

                int jhg = PlayerPrefs.GetInt("currency");

                PlayerPrefs.SetInt("currency", jhg - Precio[turboIndex]);
                    }
        else
        {
            Debug.Log("Ya posees este turbo!");
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
