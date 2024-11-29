using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumoVolcan : MonoBehaviour
{
    public ParticleSystem humoVolcan;
    public ControlPosta RR;
    public Misiones MM;

    void Start()
    {
        
    }

    void Update()
    {
      
       if (RR.CalentonJ == true && (MM.Ganaste == false && MM.nein == false))
        {
            HumearAumenta();
        }
    }

    void HumearAumenta()
    {
        var emission = humoVolcan.emission;

        // Obtén el valor actual de la tasa de emisión como un MinMaxCurve
        var rateCurve = emission.rateOverTime;

        // Incrementa el valor de la tasa de emisión utilizando Time.deltaTime
        rateCurve = new ParticleSystem.MinMaxCurve(rateCurve.constant + Time.deltaTime * 10);

        // Asigna la nueva curva de tasa de emisión
        emission.rateOverTime = rateCurve;

        // Limita la tasa de emisión para que no exceda el máximo
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Clamp(rateCurve.constant, 0, 2000));
    }

    
}
