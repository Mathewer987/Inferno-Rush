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



    private void Awake()
    {
        Modificaciones.SetActive(false);
        Principal.SetActive(true);
        engine.SetActive(false);
        //piston.SetActive(false);
        //nitro.SetActive(false);
        //aleron.SetActive(false);
        //turboCargador.SetActive(false);
        //pintura.SetActive(false);

        vehiclePointer = PlayerPrefs.GetInt("pointer");
        //PlayerPrefs.SetInt("currency", 951254632);

        // Aquí instancias el vehículo
        childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
        childObject.transform.parent = newParent.transform;
        getCarInfo();
        GM.carIndex = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName;
    }

    private void FixedUpdate()
    {
        toRotate.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        childObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

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

    public void BotonVuelta()
    {
        //if (MM.engine == false && MM.piston == false && MM.nitro == false && MM.aleron == false && MM.turbo == false && MM.pintura == false) { 
        Modificaciones.SetActive(false);
        Principal.SetActive(true);
        //}

        //else if (MM.engine == true || MM.piston == true || MM.nitro == true || MM.aleron == true || MM.turbo == true || MM.pintura == true)
        //{

        if (MM.engine == true)
        {
            Modificaciones.SetActive(true);
            engine.SetActive(false);
            piston.SetActive(false);
            nitro.SetActive(false);
            aleron.SetActive(false);
            turboCargador.SetActive(false);
            pintura.SetActive(false);
        }
        

        //}

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

            return;

        }
        currency.text = "$" + PlayerPrefs.GetInt("currency").ToString("");
        currency2.text = "$" + PlayerPrefs.GetInt("currency").ToString("");

        carInfo.text = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString() + " $ " +
                        listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carPrice.ToString();

        startButton.SetActive(false);
        buyButton.SetActive(buyButton);

    }

}
