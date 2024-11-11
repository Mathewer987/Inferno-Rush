    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PanelDataPinturaPosta : MonoBehaviour
{
    public List<Material> materialovich;
    public GameObject autoPadre; // El objeto vacío que contiene el auto instanciado
    public GameObject autinho;
    public GameObject CCR;
    public GameObject final;
    public Renderer renderer;
    public Material nuevoMaterial;
    public int MatNum;
    public MejorasManejador MM;
    public GeneralManager GM;

    private void FixedUpdate()
    {
        autinho = autoPadre.transform.GetChild(0).gameObject;
        CCR = autinho.transform.Find("Camaro con ruedas").gameObject;
        final = CCR.transform.Find("CUERPO").gameObject;
        renderer = final.GetComponent<Renderer>();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            nuevoMaterial = null;
            renderer.material = nuevoMaterial;

        }
    }

    public void Tick()
    {
        MM.pinturaSalida = true;
        GM.mat = nuevoMaterial;
    }

    public void Derecha()
    {

        if (MatNum < materialovich.Count - 1)
        {
            MatNum++;
        }

        else
        {
            MatNum = 0;
        }

        nuevoMaterial = materialovich[MatNum];

        renderer.material = nuevoMaterial;
        MM.pinturaSalida = false;

    }

    public void Izquierda()
    {
        if (MatNum > 0)
        {
            MatNum--;
        }


        nuevoMaterial = materialovich[MatNum];

        renderer.material = nuevoMaterial;
        MM.pinturaSalida = false;

    }


}
