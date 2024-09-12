using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public ControlPosta RR;
    public GameObject Aguja;
    public Text kph;
    public Text Marcha;
    private float posicionInicial = 223f, posicionFinal = -46f;
    private float posicionDeseada;
    public float velocidadVehiculo;
    public GameObject autinho;

    void Awake()
    {
        RR = autinho.GetComponent<ControlPosta>();
    }
    void Start()
    {

    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        //velocidadVehiculo = RR.KPH;
        velocidadVehiculo = RR.KPH;
        kph.text = RR.KPH.ToString("0");
        updateAguja();
    }

    public void updateAguja()
    {

        float rangoMovimiento = posicionInicial - posicionFinal;
        float temp = velocidadVehiculo / 179;
        float nuevaPosicion = posicionInicial - temp * rangoMovimiento;

        Aguja.transform.eulerAngles = new Vector3(0, 0, nuevaPosicion);

        float valorZ = Aguja.transform.eulerAngles.z;

        

        float objetivo = 71.86f;
        float umbral = 0.5f;

        if (Mathf.Abs(valorZ - objetivo) <= umbral)
        {
            Debug.Log(velocidadVehiculo);
        }
        
       
        


    }



    public void changeGear()
    {
        Marcha.text = (!RR.reverse) ? (RR.gearNum + 1).ToString() : "R";


    }

}
