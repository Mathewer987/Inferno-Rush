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

    internal enum gearBox
    {
        Automatico,
        Manual
    }
    [SerializeField] private gearBox gearChange;



    public GameManager manager;
    public bool reverse;

    public float TotalPower;
    public float wheelsRPM;
    public AnimationCurve enginePower;

    public float engineRPM;
    public float smoothTime = 0.01f;
    public float[] gears;
    public int gearNum = 0;

    public float maxRPM, minRPM;

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
    public float thrust = -20000f;

    

    public float[] slip = new float[4];

    void Start()
    {
        getObjects();
    }

    private void Update()
    {
        Shifter();
    }

    private void FixedUpdate()
    {
        AgregarFuerzaAbajo();
        AnimacionRuedas();
        Movela();
        Rotala();
        DameFriccion();
        CalcularPotencia();
    }

    private void CalcularPotencia()
    {
        RPMRuedas();
        TotalPower = (enginePower.Evaluate(engineRPM) * (gears[gearNum]) * IM.vertical) * -1;
        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);



        Movela();
    }

    private void Shifter()
    {

        if (IsGrounded())return;

        if(gearChange == gearBox.Automatico)
        {
            if (engineRPM > maxRPM && gearNum < gears.Length - 1 && !reverse)
            {
                gearNum++;
                manager.changeGear();

            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gearNum++;
                manager.changeGear();

            }
        }

        if (engineRPM < minRPM & gearNum > 0)
        {
            gearNum--;
            manager.changeGear();
        } 




    }

    private bool IsGrounded()
    {
        if (Ruedas[0].isGrounded && Ruedas[1].isGrounded && Ruedas[2].isGrounded && Ruedas[3].isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RPMRuedas()
    {
        float sum = 0;
        int R = 0;

        for (int i = 0; i < 4; i++)
        {
            sum += Ruedas[i].rpm;
            R++;
        }

        wheelsRPM = (R != 0) ? sum / R : 0;

        if (wheelsRPM > 0 && !reverse)
        {
            reverse = true;
            manager.changeGear();
        }

        if (wheelsRPM < 0 && reverse)
        {
            reverse = false;
            manager.changeGear();
        }

    }
    private void Movela()
    {


        if (drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < Ruedas.Length; i++)
            {
                Ruedas[i].motorTorque = TotalPower / 4;
            }
        }
        else if (drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < Ruedas.Length; i++)
            {
                Ruedas[i].motorTorque = TotalPower / 2;
            }
        }

        else
        {
            for (int i = 0; i < Ruedas.Length - 2; i++)
            {
                Ruedas[i].motorTorque = TotalPower / 2;
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


