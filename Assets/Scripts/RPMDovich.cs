using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPMDovich : MonoBehaviour
{
    public ControlPosta RR; // Asegúrate de que este script actualice la velocidad
    public GameObject AgujaSinTacc;
    private float posicionInicial = 185.4f, posicionFinal = -46.6f;
    private float posicionDeseada;
    public float velocidadVehiculo;
    public Sprite BotonOn;
    public Sprite BotonOff;
    public Image botonImage;
    public GameObject autinho;
    float rangoMovimiento;
    float temp;
    float nuevaPosicion;
    public bool IOP;

    void Awake()
    {
        RR = autinho.GetComponent<ControlPosta>();
        botonImage.sprite = BotonOff;

    }
    void start()
    {



    }
    private void Update() // Cambia FixedUpdate por Update
    {
        velocidadVehiculo = RR.KPH;


        if (RR.cambiopolis == true)
        {
            nuevaPosicion = AgujaSinTacc.transform.eulerAngles.z;
            IOP = true;
        }

        updateETA();


        if (velocidadVehiculo >= RR.maxSpeed - 1)
        {
            botonImage.sprite = BotonOn;
        }
        else
        {
            botonImage.sprite = BotonOff;
        }

        if (RR.cambiopolis == true)
        {
            RR.cambiopolis = false;
        }

        if (RR.Loca == 0) { }
        {
            IOP = false;
        }
    }

    public void updateETA()
    {
   

        rangoMovimiento = posicionInicial - posicionFinal;

        if (IOP == false)
        {
            temp = velocidadVehiculo / 40;
        }

        else if (IOP == true)
        {
            
        }

        if (RR.cambiopolis == false)
        {
            nuevaPosicion = posicionInicial - temp * rangoMovimiento;
        }

        AgujaSinTacc.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);
    }
}
