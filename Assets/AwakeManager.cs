using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeManager : MonoBehaviour
{
    public GameObject toRotate;
    public float rotacionVelo;
    public GameObject[] VehiCULOS;
    public int vehiclePointer = 0;

    private void Awake()
    {
        vehiclePointer = PlayerPrefs.GetInt("pointer");

        GameObject childObject = Instantiate(VehiCULOS.vehiculos[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
        childObject.transform.parent = toRotate.transform;
    }

    private void FixedUpdate()
    {
        toRotate.transform.Rotate(Vector3.up * rotacionVelo * Time.deltaTime);
    }

    public void BTNDerecho()
    {
        if(vehiclePointer < VehiCULOS.vehicles.Length-1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            GameObject childObject = Instantiate(VehiCULOS.vehiculos[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = toRotate.transform;


        }


    }

    public void BTNIzquierdo()
    {
        if (vehiclePointer > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            GameObject childObject = Instantiate(VehiCULOS.vehiculos[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = toRotate.transform;


        }


    }
}
