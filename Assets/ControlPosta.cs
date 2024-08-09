using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPosta : MonoBehaviour
{
    internal enum driveType{
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
     }

    [SerializeField]private driveType drive; 

    private inputManager IM;
    public WheelCollider[] Ruedas = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float motorTorque = -200;
    public float doblarMax = 30;
    // Start is called before the first frame update
    void Start()
    {
        getObjects();
    }

    private void FixedUpdate()
    {

        AnimacionRuedas();
        Movela();
        Rotala();
       
    }

    private void Movela()
    {

        float PotenciaTotal;

        if(drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < Ruedas.Length; i++)
            {
                Ruedas[i].motorTorque = IM.vertical * (motorTorque / 4) ;
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
    }

    private void Rotala()
    {
        for (int i = 0; i < Ruedas.Length - 2; i++)
        {
            Ruedas[i].steerAngle = IM.horizontal * doblarMax;
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
    }
}
