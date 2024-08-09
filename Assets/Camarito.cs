using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camarito : MonoBehaviour
{

    public GameObject Player;
    public GameObject Hijito;
    public float Velocidad;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hijito = Player.transform.Find("Camardopolis").gameObject;
    }

    private void FixedUpdate()
    {
        SeguirConLike();
    }

    private void SeguirConLike()
    {

        gameObject.transform.position = Vector3.Lerp(transform.position, Hijito.transform.position, Time.deltaTime * Velocidad);
        gameObject.transform.LookAt(Player.gameObject.transform.position);
    }
}
