using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autinhos : MonoBehaviour
{

    public GameObject[] autinhos;
    public RPMDovich RD;
    public GameManager GM;
    void Awake()
    {
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