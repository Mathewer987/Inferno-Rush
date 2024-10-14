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
    public GameObject PRI;
    public GameObject PRL;

    public Image botonImage;
    public GameObject autinho;
    float rangoMovimiento;
    float temp;
    float nuevaPosicion;

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
        PRI.SetActive(RR.yupi);

        velocidadVehiculo = RR.KPH;
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
            AgujaSinTacc.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);
            RR.cambiopolis = false;

        }

        if (RR.CalentonJ == true)
        {
            PRI.SetActive(false);
            PRL.SetActive(false);


        }

    }

    public void updateETA()
    {
        // Calcular el rango de movimiento de la aguja
        rangoMovimiento = posicionInicial - posicionFinal;

        float lpm = RR.VCambios[RR.gearNum];

        // Calcular la posición objetivo de la aguja para la velocidad actual
        float temp = velocidadVehiculo / lpm;
        float posicionObjetivo = posicionInicial - temp * rangoMovimiento;

        // Interpolar suavemente entre la posición actual y la posición objetivo
        nuevaPosicion = Mathf.Lerp(nuevaPosicion, posicionObjetivo, Time.deltaTime * 5f);

        // Actualizar la rotación de la aguja
        AgujaSinTacc.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);
    }

}
