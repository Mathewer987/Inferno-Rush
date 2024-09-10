using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPMDovich : MonoBehaviour
{
    public ControlPosta RR; // Asegúrate de que este script actualice la velocidad
    public GameObject AgujaSinTacc;
    private float posicionInicial = 215f, posicionFinal = -46.6f;
    private float posicionDeseada;
    public float velocidadVehiculo; 

    // Update is called once per frame
    private void Update() // Cambia FixedUpdate por Update
    {
        velocidadVehiculo = RR.KPH;
        updateETA();
    }

    public void updateETA()
    {
   

        float rangoMovimiento = posicionInicial - posicionFinal;
        float temp = velocidadVehiculo / 40;
        float nuevaPosicion = posicionInicial - temp * rangoMovimiento;

        AgujaSinTacc.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);
    }
}
