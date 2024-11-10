using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AwakeManager : MonoBehaviour
{
    public GameObject toRotate;
    public GameObject buyButton;
    public GameObject startButton;
    public float rotateSpeed;
    public vehicleList4 listOfVehicles;
    public int vehiclePointer = 0;
    public Text currency;
    public Text currency2;
    public Text currency3;
    public Text currency4;
    public Text currency5;
    public Text currency6;


    public Text carInfo;
    public GeneralManager GM;
    public GameObject newParent;
    public GameObject childObject;
    public GameObject Modificaciones;
    public GameObject Principal;

    public GameObject engine;
    public GameObject piston;
    public GameObject nitro;
    public GameObject aleron;
    public GameObject turboCargador;
    public GameObject pintura;


    public MejorasManejador MM;
    public int CDF1;
    public int CDF2;
    public int CDF3;
    public int CDF5;

    public int P1;
    public int P2;
    public int P3;
    public int P5;

    public int CFSinSuTurbo;
    public float CFPosta;

    public float PSIBase = 14.7f;
    public int Peso;

    private void Awake()
    {
        Modificaciones.SetActive(false);
        Principal.SetActive(true);
        engine.SetActive(false);
        piston.SetActive(false);
        nitro.SetActive(false);
        //aleron.SetActive(false);
        turboCargador.SetActive(false);
        pintura.SetActive(false);

        vehiclePointer = PlayerPrefs.GetInt("pointer");
        //PlayerPrefs.SetInt("currency", 951254632);

        // Aquí instancias el vehículo
        childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
        childObject.transform.parent = newParent.transform;
        getCarInfo();
        GM.carIndex = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName;
      
        //Parte caballos de fuerza variables
        
        CDF1 = PlayerPrefs.GetInt("CaballosDeFuerza");
        CDF2 = PlayerPrefs.GetInt("CaballosDeFuerzaP");
        CDF3 = PlayerPrefs.GetInt("CaballosDeFuerzaN");
        CDF5 = PlayerPrefs.GetInt("PSIss");

        //Parte pesos variables

        P1 = PlayerPrefs.GetInt("PesoMotor");
        P2 = PlayerPrefs.GetInt("PesoPiston");
        P3 = PlayerPrefs.GetInt("PesoNitro");
        P5 = PlayerPrefs.GetInt("PesoTurbo");

        CFSinSuTurbo = CDF1 + CDF2 + CDF3;
    }

    

    private void FixedUpdate()
    {
        toRotate.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        childObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        //Parte caballos de fuerza variables

        CDF1 = PlayerPrefs.GetInt("CaballosDeFuerza");
        CDF2 = PlayerPrefs.GetInt("CaballosDeFuerzaP");
        CDF3 = PlayerPrefs.GetInt("CaballosDeFuerzaN");
        CDF5 = PlayerPrefs.GetInt("PSIss");

        //Parte pesos variables

        P1 = PlayerPrefs.GetInt("PesoMotor");
        P2 = PlayerPrefs.GetInt("PesoPiston");
        P3 = PlayerPrefs.GetInt("PesoNitro");
        P5 = PlayerPrefs.GetInt("PesoTurbo");

        CFSinSuTurbo = CDF1 + CDF2 + CDF3;

        Peso = P1 + P2 + P5;

        CFPosta = CalcularCaballosDeFuerza(CFSinSuTurbo, PSIBase, CDF5);
    }
    public float CalcularCaballosDeFuerza(float hpBase, float psiBase, float psiActual)
    {
        // Calcula el aumento de caballos de fuerza en función de la presión (PSI)
        float nuevaPotencia = hpBase * Mathf.Sqrt((psiBase + psiActual) / psiBase);
        return nuevaPotencia;
    }



    public void rightButton()
    {
        if (vehiclePointer < listOfVehicles.vehicles.Length - 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = newParent.transform;
            getCarInfo();
            GM.carIndex = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName;

        }
    }

    public void ModificacionesBoton()
    {
        Modificaciones.SetActive(true);
        Principal.SetActive(false);

    }

    public void BotonVueltaEngine()
    {
        MM.engine = false;
        Modificaciones.SetActive(true);
        engine.SetActive(false);
        piston.SetActive(false);
        nitro.SetActive(false);
        aleron.SetActive(false);
        turboCargador.SetActive(false);
        pintura.SetActive(false);

    }

    public void BotonVueltaPiston()
    {
        MM.piston = false;
        Modificaciones.SetActive(true);
        engine.SetActive(false);
        piston.SetActive(false);
        nitro.SetActive(false);
        //aleron.SetActive(false);
        turboCargador.SetActive(false);
        pintura.SetActive(false);

    }

    public void BotonVueltanitro()
    {
        MM.nitro = false;
        Modificaciones.SetActive(true);
        engine.SetActive(false);
        piston.SetActive(false);
        nitro.SetActive(false);
        aleron.SetActive(false);
        turboCargador.SetActive(false);
        pintura.SetActive(false);

    }

    public void BotonVueltaaleron()
    {
        MM.aleron = false;
        Modificaciones.SetActive(true);
        engine.SetActive(false);
        piston.SetActive(false);
        nitro.SetActive(false);
        aleron.SetActive(false);
        turboCargador.SetActive(false);
        pintura.SetActive(false);

    }

    public void BotonVueltaturbo()
    {
        MM.turbo= false;
        Modificaciones.SetActive(true);
        engine.SetActive(false);
        piston.SetActive(false);
        nitro.SetActive(false);
        //aleron.SetActive(false);
        turboCargador.SetActive(false);
        pintura.SetActive(false);

    }

    public void BotonVueltapintura()
    {
        if (MM.pinturaSalida == true)
        {
            MM.pintura = false;
            Modificaciones.SetActive(true);
            engine.SetActive(false);
            piston.SetActive(false);
            nitro.SetActive(false);
            //aleron.SetActive(false);
            turboCargador.SetActive(false);
            pintura.SetActive(false);
        }
       

    }
    public void BotonVueltaPosta()
    {
        if (MM.engine == false && MM.piston == false && MM.nitro == false && MM.aleron == false && MM.turbo == false && MM.pintura == false) { 
        Modificaciones.SetActive(false);
        Principal.SetActive(true);
        }
    }

    public void leftButton()
    {
        if (vehiclePointer > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = newParent.transform;
            getCarInfo();
            GM.carIndex = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName;

        }
    }

    public void startGameButton()
    {
        SceneManager.LoadScene("Prueba Manejo");
    }

    public void BuyButton()
    {


        if (PlayerPrefs.GetInt("currency") >= listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carPrice)
        {
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") - listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carPrice);

            PlayerPrefs.SetString(listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString(),
                                    listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString());
            getCarInfo();
        }

    }

    public void getCarInfo()
    {
        if (listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString() ==
            PlayerPrefs.GetString(listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString()))
        {
            carInfo.text = "Owned";
            startButton.SetActive(true);
            buyButton.SetActive(false);
            currency.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
            currency2.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
            currency3.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
           // currency4.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
            currency5.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
            currency6.text = "$" + PlayerPrefs.GetInt("currency").ToString("");

            return;

        }
        currency.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currency2.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currency3.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
       // currency4.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currency5.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currency6.text = "$" + PlayerPrefs.GetInt("currency").ToString("");

        carInfo.text = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString() + " $ " +
                        listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carPrice.ToString();

        startButton.SetActive(false);
        buyButton.SetActive(buyButton);

    }

}
