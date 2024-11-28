using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camaras : MonoBehaviour
{
    public Camera[] cameras; // Lista de cámaras
    private int currentCameraIndex = 0; // Índice de la cámara activa

    void Start()
    {
        // Asegurarse de que solo la primera cámara está activa al iniciar
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    void Update()
    {
        // Cambiar de cámara al presionar una tecla (por ejemplo, la tecla C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Desactivar la cámara actual
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Cambiar al siguiente índice
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Activar la nueva cámara
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
