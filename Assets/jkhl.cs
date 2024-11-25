using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jkhl : MonoBehaviour
{
    public GameObject autoPadre; // El objeto vacío que contiene el auto instanciado
    public GameObject autinho;
    public GameObject CCR;
    public GameObject final;
    public Renderer renderer;
    public PanelDataPinturaPosta PP;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        autinho = autoPadre.transform.GetChild(0).gameObject;
        CCR = autinho.transform.Find("Camaro con ruedas").gameObject;

        //final = CCR.transform.Find("CUERPO").gameObject;


        Transform[] cuerpos = CCR.GetComponentsInChildren<Transform>(true);

        foreach (Transform cuerpo in cuerpos)
        {
            // Verificamos si el GameObject está activo
            if (cuerpo.name == "CUERPO" && cuerpo.gameObject.activeSelf)
            {
                final = cuerpo.gameObject;
                break; // Salimos del bucle al encontrar el activo
            }
        }

        renderer = final.GetComponent<Renderer>();
        renderer.material = PP.materialovich[PlayerPrefs.GetInt("IndexPintura")];
    }
}
