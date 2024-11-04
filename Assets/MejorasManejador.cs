using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MejorasManejador : MonoBehaviour
{
    public bool engine;
    public bool piston;
    public bool nitro;
    public bool aleron;
    public bool turbo;
    public bool pintura;

    public Text currencyE;
    public Text currencyP;
    public Text currencyN;
    public Text currencyA;
    public Text currencyTC;
    public Text currencyPintura;

    public AwakeManager AM;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currencyE.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currencyP.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currencyN.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        //currencyA.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currencyTC.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        //currencyPintura.text = "$" + PlayerPrefs.GetInt("currency").ToString("");

    }

    public void botonEngine()
    {
        AM.Modificaciones.SetActive(false);
        AM.engine.SetActive(true);
        
        engine = true;
    }

    public void botonPiston()
    {
        AM.Modificaciones.SetActive(false);
        AM.piston.SetActive(true);
        piston = true;

    }

    public void botonNitro()
    {
        AM.Modificaciones.SetActive(false);
        AM.nitro.SetActive(true);
        nitro = true;

    }

    public void botonAleron()
    {
        AM.Modificaciones.SetActive(false);
        AM.aleron.SetActive(true);
        aleron = true;

    }

    public void botonTurbo()
    {
        AM.Modificaciones.SetActive(false);
        AM.turboCargador.SetActive(true);
        turbo = true;

    }

    public void botonPintura()
    {
        AM.Modificaciones.SetActive(false);
        AM.pintura.SetActive(true);
        pintura = true;

    }



}
