using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ControlPosta RR;
    public GameObject Aguja;
    private float posicionInicial = 223f, posicionFinal = -46f;
    private float posicionDeseada;
    public float velocidadVehiculo;
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        velocidadVehiculo = RR.KPH;
        updateAguja();
    }

    public void updateAguja()
    {

        float rangoMovimiento = posicionInicial - posicionFinal;

        float temp = velocidadVehiculo / 179;


        float nuevaPosicion = posicionInicial - temp * rangoMovimiento;


        Aguja.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);

    }

}
