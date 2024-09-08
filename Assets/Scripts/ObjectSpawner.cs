using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject monedaPrefab; // Prefab de la moneda
    public int cantidadDeMonedas = 10; // Cantidad de monedas a instanciar
    public MeshCollider meshCollider; // MeshCollider de la calle
    public float alturaInicial = 10f; // Altura desde la cual se lanzará el rayo
    public int maxIntentos = 10; // Número máximo de intentos para encontrar una posición válida
    public float HIV = 5f; // Altura adicional sobre la carretera
    public float velocidadRotacion = 100f; // Velocidad de rotación de las monedas
    public GameObject[] monedasInstanciadas; // Array para almacenar las monedas instanciadas
    private int HZ = 0; // Contador para llevar la cuenta de las monedas instanciadas
    public Text puntaje;
    public CollisionObjeto NT;
    public int VoyPaLla;
    private Misiones MT;



    void Start()
    {
        maxIntentos = cantidadDeMonedas;
        monedasInstanciadas = new GameObject[cantidadDeMonedas];
        puntaje.text = "0/" + (cantidadDeMonedas - 3);
        InstanciarMonedas();
        
    }

    void Update()
    {
        

        
        for (int i = 0; i < monedasInstanciadas.Length; i++)
        {
            if (monedasInstanciadas[i] != null)
            {
                // Rotar las monedas alrededor del eje Y
                monedasInstanciadas[i].transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
            }
        }
        
        puntaje.text = VoyPaLla.ToString() + "/" + (cantidadDeMonedas - 3);
        
    }

    void InstanciarMonedas()
    {
        

        
        for (int i = 0; i < cantidadDeMonedas; i++)
        {
            Vector3 puntoAleatorioEnCarretera = GenerarPuntoAleatorioEnCarretera();

            if (puntoAleatorioEnCarretera != Vector3.zero)
            {
                // Instanciar la moneda en la posición encontrada
                GameObject monedaInstanciada = Instantiate(monedaPrefab, puntoAleatorioEnCarretera, Quaternion.identity);

                // Almacenar la referencia de la moneda instanciada en el array
                monedasInstanciadas[HZ] = monedaInstanciada;
                HZ++;
            }
            else
            {
                Debug.LogWarning("No se encontró una posición válida para la moneda después de varios intentos.");
            }
        }
    }

    Vector3 GenerarPuntoAleatorioEnCarretera()
    {
        // Obtener los límites del MeshCollider
        Bounds limites = meshCollider.bounds;

        // Intentar encontrar una posición válida en la carretera
        for (int intentos = 0; intentos < maxIntentos; intentos++)
        {
            // Generar una posición aleatoria dentro de los límites del MeshCollider
            Vector3 posicionAleatoria = new Vector3(
                Random.Range(limites.min.x, limites.max.x),
                limites.max.y + alturaInicial, // Un poco por encima de la carretera
                Random.Range(limites.min.z, limites.max.z)
            );

            // Lanzar un rayo hacia abajo desde la posición aleatoria
            Ray rayo = new Ray(posicionAleatoria, Vector3.down);
            RaycastHit hitInfo;

            // Si el rayo choca con el MeshCollider de la carretera
            if (meshCollider.Raycast(rayo, out hitInfo, Mathf.Infinity))
            {
                // Devolver el punto de intersección ajustado a una altura ligeramente sobre la carretera
                Vector3 puntoFinal = hitInfo.point;
                puntoFinal.y += HIV; // Ajusta la altura adicional sobre la carretera
                return puntoFinal;
            }
        }

        return Vector3.zero; // No se encontró un punto válido después de varios intentos
    }
    
}
