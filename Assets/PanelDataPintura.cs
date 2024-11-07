using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PanelDataPintura : MonoBehaviour
{
    public List<Material> materialovich;
    public GameObject autoPadre; // El objeto vacío que contiene el auto instanciado
    public GameObject autinho;
    public GameObject CCR;
    public GameObject final;
    public Renderer renderer;
    public Material nuevoMaterial;
    public List<Button> miniDatas;

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

    public void SeleccionArgentina(Button clickedButton)
    {
        int buttonIndex = miniDatas.IndexOf(clickedButton);

        Debug.Log(buttonIndex);
           
        
    }
}
