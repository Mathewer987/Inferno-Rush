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
    public Transform Llanta3;
    public Transform Llanta4;
    public float velocityAct;
    public int VelocityMax = 2000;

    public GameObject LlantaAdDer;
    public GameObject LlantaAdIzq;
    public GameObject LlantaAtrDer;
    public GameObject LlantaAtrIzq;

    public Transform Autito;

    float VelocidadAtras;

    public int driftLevel = 1;

    private WheelFrictionCurve driftForwardFriction;
    private WheelFrictionCurve driftSidewaysFriction;
    private WheelFrictionCurve normalForwardFriction;
    private WheelFrictionCurve normalSidewaysFriction;


    // Start is called before the first frame update
    void Start()
    {
        normalForwardFriction = Rueda3.forwardFriction;
        normalSidewaysFriction = Rueda3.sidewaysFriction;

        driftForwardFriction = new WheelFrictionCurve();
        driftForwardFriction.extremumSlip = 1f;
        driftForwardFriction.extremumValue = 1f;
        driftForwardFriction.asymptoteSlip = 1f;
        driftForwardFriction.asymptoteValue = 1f;
        driftForwardFriction.stiffness = 0.1f;

        driftSidewaysFriction = new WheelFrictionCurve();
        driftSidewaysFriction.extremumSlip = 0.5f;
        driftSidewaysFriction.extremumValue = 1f;
        driftSidewaysFriction.asymptoteSlip = 0.5f;
        driftSidewaysFriction.asymptoteValue = 1f;
        driftSidewaysFriction.stiffness = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        velocityAct = (2 * Mathf.PI * Rueda3.radius) * Rueda3.rpm * 60 / 100;
        Llanta1.localEulerAngles = new Vector3(0, Rueda1.steerAngle, 0);
        Llanta2.localEulerAngles = new Vector3(0, Rueda2.steerAngle, 0);
        ActualizacionRuedas();
        VelocidadAtras = velocityAct * -1;

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector3 ntPosicion = transform.position;
            ntPosicion.y += 4;
            transform.position = ntPosicion;

        }
    }

    private void FixedUpdate()
    {
        if (VelocidadAtras < VelocityMax)
        {
            Rueda3.motorTorque = Velocidad * Input.GetAxis("Vertical") * -1;
            Rueda4.motorTorque = Velocidad * Input.GetAxis("Vertical") * -1;
        }
        else
        {
            Rueda3.motorTorque = 0;
            Rueda4.motorTorque = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Ajustar la rigidez de las fricciones de derrape basándonos en driftLevel
            float driftStiffness = Mathf.Clamp(1f / driftLevel, 0.1f, 1f);
            driftForwardFriction.stiffness = driftStiffness;
            driftSidewaysFriction.stiffness = driftStiffness;

            Rueda3.forwardFriction = driftForwardFriction;
            Rueda3.sidewaysFriction = driftSidewaysFriction;
            Rueda4.forwardFriction = driftForwardFriction;
            Rueda4.sidewaysFriction = driftSidewaysFriction;
        }

        else
        {
            Rueda3.forwardFriction = normalForwardFriction;
            Rueda3.sidewaysFriction = normalSidewaysFriction;
            Rueda4.forwardFriction = normalForwardFriction;
            Rueda4.sidewaysFriction = normalSidewaysFriction;
            Rueda1.steerAngle = 40 * Input.GetAxis("Horizontal");
            Rueda2.steerAngle = 40 * Input.GetAxis("Horizontal");
        }

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

    private void ActualizacionRuedas()
    {
        ActualizarPosicionLlantas(Rueda1, Llanta1);
        ActualizarPosicionLlantas(Rueda2, Llanta2);
        ActualizarPosicionLlantas(Rueda3, Llanta3);
        ActualizarPosicionLlantas(Rueda4, Llanta4);
    }

    private void ActualizarPosicionLlantas(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        transform.rotation = quat;
    }
}
