using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibracionesTecla : MonoBehaviour
{
     public CameraShake cameraShake; // Arrastra la cámara con el script aquí.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Al presionar Espacio
        {
            cameraShake.TriggerShake(0.5f, 0.2f);
        }
    }
    
}
