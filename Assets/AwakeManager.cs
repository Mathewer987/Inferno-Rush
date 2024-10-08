using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AwakeManager : MonoBehaviour
{
    public GameObject toRotate;
    public float rotacionVelo;
    public GameObject[] VehiCULOS;
    public GameObject player;
    public int vehiclePointer = 0;
    public int dillom = -1;




    private void Awake()
    {
        foreach (GameObject vehiculo in VehiCULOS)
        {
            if (vehiculo.tag == "Player")
            {
                vehiculo.SetActive(false);
            }
        }

        player = VehiCULOS[vehiclePointer];
        player.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Prueba Manejo")
        {
            toRotate.transform.Rotate(Vector3.up * rotacionVelo * Time.deltaTime);

            foreach (GameObject gh in VehiCULOS)
            {
                gh.transform.Rotate(Vector3.up * rotacionVelo * Time.deltaTime);

            }
        }
        


    }

    public void BTNDerecho()
    {
        if (vehiclePointer < VehiCULOS.Length - 1)
        {
            player.SetActive(false);
            player = null;
            vehiclePointer++;
            player = VehiCULOS[vehiclePointer];
            player.SetActive(true);
        }

       
    }

    public void BTNIzquierdo()
    {
        if (vehiclePointer > 0)
        {
            player.SetActive(false);
            player = null;
            vehiclePointer--;
            player = VehiCULOS[vehiclePointer];
            player.SetActive(true);
        }


    }

    public void buenovich()
    {
        for (int i = 0; i < VehiCULOS.Length; i++)
        {
            // Si el GameObject está activado
            if (VehiCULOS[i].activeSelf)
            {
                dillom = i;  // Guardamos el índice
            }
        }
        GeneralManager.Instance.carIndex = vehiclePointer;
        SceneManager.LoadScene("Prueba Manejo");
    }
}
