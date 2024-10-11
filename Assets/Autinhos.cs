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
    public GameObject objectToMakeChild; // El objeto que quieres hacer hijo del auto activo



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

                objectToMakeChild.transform.SetParent(autinho.transform);


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
