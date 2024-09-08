using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObjeto : MonoBehaviour
{
    private ObjectSpawner objectSpawner;
    private Misiones MT;


    private void Start()
    {
        
        // Obtener la referencia al script ObjectSpawner
        objectSpawner = FindObjectOfType<ObjectSpawner>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitbox"))
        {
            Debug.Log("¡El auto ha recogido una moneda!");

            // Verifica si el objectSpawner ha sido encontrado
            if (objectSpawner != null)
            {
                objectSpawner.VoyPaLla++; // Incrementa el puntaje en ObjectSpawner
                Debug.Log("Puntaje actualizado: " + objectSpawner.VoyPaLla);
            }
            else
            {
                Debug.LogWarning("No se encontró una instancia de ObjectSpawner.");
            }

            // Destruye la moneda después de ser recogida
            Destroy(gameObject);
        }
    }
}
