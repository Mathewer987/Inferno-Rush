using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPosta : MonoBehaviour
{

    public WheelCollider[] Ruedas = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float motorTorque = -200;
    public float doblarMax = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        AnimacionRuedas();

        if (Input.GetKey(KeyCode.W))
        {
            for (int i = 0; i < Ruedas.Length; i++)
            {
                Ruedas[i].motorTorque = motorTorque;
            }
        }
        
        if (Input.GetAxis("Horizontal") !=0)
        {
            for (int i = 0; i < Ruedas.Length - 2; i++)
            {
                Ruedas[i].steerAngle = Input.GetAxis("Horizontal") * doblarMax;
            }
        }
        else
        {
            for (int i = 0; i < Ruedas.Length - 2; i++)
            {
                Ruedas[i].steerAngle = 0;
            }
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
}
