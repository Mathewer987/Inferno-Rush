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
    public Text carInfo;
    public GeneralManager GM;
    public GameObject newParent;
    public GameObject childObject;



    private void Awake()
    {
        vehiclePointer = PlayerPrefs.GetInt("pointer");
        PlayerPrefs.SetInt("currency", 150000);

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

            return;

        }
        currency.text = "$" + PlayerPrefs.GetInt("currency").ToString("");

        carInfo.text = listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carName.ToString() + " $ " +
                        listOfVehicles.vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<ControlPosta>().carPrice.ToString();

        startButton.SetActive(false);
        buyButton.SetActive(buyButton);

    }

}
