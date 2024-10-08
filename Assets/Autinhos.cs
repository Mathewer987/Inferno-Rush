using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autinhos : MonoBehaviour
{

    public GameObject[] autinhos;
    public RPMDovich RD;
    public GameManager GM;
    public AwakeManager GG;
    string nombreEscenaObjetivo = "Prueba Manejo";


    
    void Awake()
    {
        
            autinhos[GeneralManager.Instance.carIndex].SetActive(true);

            Debug.Log("asd");
        

        foreach (GameObject autinho in autinhos)
        {
            if (autinho.activeInHierarchy)
            {
                RD.autinho = autinho;
                GM.autinho = autinho;

            }
            else
            {
                Debug.Log(autinho.name + " no está activo.");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
