using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAuto : MonoBehaviour
{
    public WheelCollider Rueda1;
    public WheelCollider Rueda2;
    public WheelCollider Rueda3;
    public WheelCollider Rueda4;
    public int Velocidad = 150;
    public int Frenar = 50;
    public Transform Llanta1;
    public Transform Llanta2;
    public float velocityAct;
    public int VelocityMax = 2000;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        velocityAct = (2 * Mathf.PI * Rueda3.radius) * Rueda3.rpm * 60 / 100;
        Llanta1.localEulerAngles = new Vector3(0, Rueda1.steerAngle, 0);
        Llanta2.localEulerAngles = new Vector3(0, Rueda2.steerAngle, 0);
    }

    private void FixedUpdate()
    {
        Rueda1.steerAngle = 40 * Input.GetAxis("Horizontal");
        Rueda2.steerAngle = 40 * Input.GetAxis("Horizontal");
        Rueda3.motorTorque = Velocidad * Input.GetAxis("Vertical") * -1;
        Rueda4.motorTorque = Velocidad * Input.GetAxis("Vertical") * -1;

        if (Input.GetAxis("Vertical") == 0)
        {
            Rueda3.brakeTorque = Frenar;
            Rueda4.brakeTorque = Frenar;
        }

        else
        {
            Rueda3.brakeTorque = 0;
            Rueda4.brakeTorque = 0;

        }
    }
}
