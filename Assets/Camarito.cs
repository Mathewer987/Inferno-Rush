using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camarito : MonoBehaviour
{

    private GameObject Player;
    private ControlPosta RR;
    private GameObject Hijito;
    private GameObject cameraConstarint;
    private float Velocidad;
    public float defaltFOV = 0, desiredFOV = 0;
    [Range (0,5)]public float tiempoFluido = 0;  

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Hijito = Player.transform.Find("Camardopolis").gameObject;
        RR = Player.GetComponent<ControlPosta>();
        defaltFOV = Camera.main.fieldOfView;
    }

    private void FixedUpdate()
    {
        SeguirConLike();
        BoosteaFOV();
    }

    private void SeguirConLike()
    {
        Velocidad = Mathf.Lerp(Velocidad, RR.KPH / 2, Time.deltaTime);

        gameObject.transform.position = Vector3.Lerp(transform.position, Hijito.transform.position, Time.deltaTime * Velocidad);
        gameObject.transform.LookAt(Player.gameObject.transform.position);
    }

    private void BoosteaFOV()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, Time.deltaTime * tiempoFluido);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaltFOV, Time.deltaTime * tiempoFluido);

        }
    }
}
