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
    int SXO = 1;
    public float smoothSpeed = 2.0f; // Velocidad de la interpolación suave
    public float targetAngle;


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
        temp = velocidadVehiculo / 40;

        // Si IOP es false, queremos mover la aguja de manera suave y dejarla en la posición correcta
        if (IOP == false && SXO == 1)
        {
            // Calcula el ángulo objetivo en función de la velocidad actual
            float targetAngle = posicionInicial - temp * rangoMovimiento;

            // Mueve suavemente la aguja hacia esa posición
            nuevaPosicion = Mathf.Lerp(nuevaPosicion, targetAngle, Time.deltaTime * smoothSpeed);

            // Actualiza la rotación de la aguja
            AgujaSinTacc.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);

            // Al finalizar, la aguja se quedará en su nueva posición
        }
        // Si IOP es true, también queremos mover la aguja suavemente a la posición deseada
        else if (IOP == true)
        {
            SXO = 0;

                    // Calcula el ángulo objetivo para el cambio de marcha o transición especial
            targetAngle = (posicionInicial + temp * rangoMovimiento) ;

            // Suaviza la transición de la aguja hacia el nuevo ángulo objetivo
            nuevaPosicion = Mathf.Lerp(nuevaPosicion, targetAngle, Time.deltaTime * smoothSpeed);

            // Actualiza la rotación de la aguja
            AgujaSinTacc.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);

            // Cambia el estado de SXO para controlar el cambio
            SXO = 1;
            
            


        }
    }
}
