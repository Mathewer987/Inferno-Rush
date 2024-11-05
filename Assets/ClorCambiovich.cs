using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClorCambiovich : MonoBehaviour
{
    public Material nuevoMaterial; // El material que deseas aplicar
    public GameObject autoPadre; // El objeto vacío que contiene el auto instanciado
    public GameObject autinho;
    public GameObject CCR;
    public GameObject final;
    public Renderer renderer;


    public void Start()
    {

    }

    private void FixedUpdate()
    {
        autinho = autoPadre.transform.GetChild(0).gameObject;
        CCR = autinho.transform.Find("Camaro con ruedas").gameObject;
        final = CCR.transform.Find("CUERPO").gameObject;
        renderer = final.GetComponent<Renderer>();
    }

    public void ntla()
    {
        renderer.material = nuevoMaterial;
    }
}
