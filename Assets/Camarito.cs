using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camarito : MonoBehaviour
{

    private GameObject Player;
    private ControlPosta RR;
    private GameObject Hijito;
    private GameObject cameraConstarint;
    public float Velocidad;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hijito = Player.transform.Find("Camardopolis").gameObject;
        RR = Player.GetComponent<ControlPosta>();
    }

    private void FixedUpdate()
    {
        SeguirConLike();

        Velocidad = (RR.KPH >= 50) ? 20 : RR.KPH / 4;
    }

    private void SeguirConLike()
    {

        gameObject.transform.position = Vector3.Lerp(transform.position, Hijito.transform.position, Time.deltaTime * Velocidad);
        gameObject.transform.LookAt(Player.gameObject.transform.position);
    }
}
