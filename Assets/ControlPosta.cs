using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPosta : MonoBehaviour
{
    internal enum driveType {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }

    [SerializeField] private driveType drive;

    private inputManager IM;
    private GameObject CentroDeMasa;
    public WheelCollider[] Ruedas = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float motorTorque = -200;
    public float doblarMax = 4;
    public float radius = 6;
    private Rigidbody rigidbody;
    public float KPH;
    public float FuerzaAbajo = 50;
    public float fuerzaDeFreno;
    public float thrust = -1000f;

    public float[] slip = new float[4];

    void Start()
    {
        getObjects();
    }



    private void FixedUpdate()
    {
        AgregarFuerzaAbajo();
        AnimacionRuedas();
        Movela();
        Rotala();
        DameFriccion();
    }
    private void Movela()
    {

        float PotenciaTotal;

        if (drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < Ruedas.Length; i++)
            {
                Ruedas[i].motorTorque = IM.vertical * (motorTorque / 4);
            }
        }
        else if (drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < Ruedas.Length; i++)
            {
                Ruedas[i].motorTorque = IM.vertical * (motorTorque / 2);
            }
        }

        else
        {
            for (int i = 0; i < Ruedas.Length - 2; i++)
            {
                Ruedas[i].motorTorque = IM.vertical * (motorTorque / 2);
            }
        }

        KPH = rigidbody.velocity.magnitude * 3.6f;

        if (IM.FrenoDeMano)
        {
            Ruedas[3].brakeTorque = Ruedas[2].brakeTorque = fuerzaDeFreno;
        }
        else
        {
            Ruedas[3].brakeTorque = Ruedas[2].brakeTorque = 0;

        }

        if (IM.boosting)
        {
            rigidbody.AddForce(Vector3.forward * thrust);
        }
    }
    private void Rotala()
    {

        if (IM.horizontal > 0)
        {
            //rear tracks size is set to 1.Sf wheel base has been set to 2.55F
            Ruedas[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            Ruedas[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }

        else if (IM.horizontal < 0)
        {
            Ruedas[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
            Ruedas[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            //transform.Rotate (Vector3.up steerHelping):
        }

        else
        {
            Ruedas[0].steerAngle = 0;
            Ruedas[1].steerAngle = 0;
        }


    }
    void AnimacionRuedas()
    {
        Vector3 PosicionRueda = Vector3.zero;
        Quaternion RotacionRueda = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            Ruedas[i].GetWorldPose(out PosicionRueda, out RotacionRueda);
            wheelMesh[i].transform.position = PosicionRueda;
            wheelMesh[i].transform.rotation = RotacionRueda;

        }
    }
    private void getObjects()
    {
        IM = GetComponent<inputManager>();
        rigidbody = GetComponent<Rigidbody>();
        CentroDeMasa = GameObject.Find("Masa");
        rigidbody.centerOfMass = CentroDeMasa.transform.localPosition;
    }
    private void AgregarFuerzaAbajo()
    {
        rigidbody.AddForce(-transform.up * FuerzaAbajo * rigidbody.velocity.magnitude);
   
    }
    private void DameFriccion()
    {
        for (int i = 0; i < Ruedas.Length; i++)
        {
            WheelHit wheelHit;
            Ruedas[i].GetGroundHit(out wheelHit);

            slip[i] = wheelHit.forwardSlip;
        }
    }

}


