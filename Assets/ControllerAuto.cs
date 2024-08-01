using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAuto : MonoBehaviour
{
    public WheelCollider Rueda1;
    public WheelCollider Rueda2;
    public int Velocidad = 150;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rueda1.motorTorque = Velocidad * Input.GetAxis("Vertical");
        Rueda2.motorTorque = Velocidad * Input.GetAxis("Vertical");
    }
}
