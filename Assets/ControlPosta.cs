using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPosta : MonoBehaviour
{
    internal enum driveType
    {
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

    private WheelFrictionCurve fowardFriction, sidewaysFriction;

    public GameManager manager;
    public bool reverse;
    public float handBrakeFrictionMultiplier = 2f;
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
        ajustarTraccion();
        ChequeaGiroRueda();
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

        if (IsGrounded()) return;

        if (gearChange == gearBox.Automatico)
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

    public float handBrakeFriction = 0;
    private float driftFactor;

       void ajustarTraccion()
       {

        float driftSmothFactor =.7f * Time.deltaTime;


        if (IM.FrenoDeMano) {

            sidewaysFriction = Ruedas[0].sidewaysFriction;
            fowardFriction = Ruedas[0].forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = fowardFriction.extremumValue = fowardFriction.asymptoteValue =
                Mathf.SmoothDamp(fowardFriction.asymptoteValue, driftFactor + handBrakeFrictionMultiplier, ref velocity, driftSmothFactor);

            for (int i = 0; i < 4; i++)
            {
                Ruedas[i].sidewaysFriction = sidewaysFriction;
                Ruedas[i].forwardFriction = fowardFriction;
            }

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = fowardFriction.extremumValue = fowardFriction.asymptoteValue = 1.1f;


            for (int i = 0; i < 2; i++)
            {
                Ruedas[i].sidewaysFriction = sidewaysFriction;
                Ruedas[i].forwardFriction = fowardFriction;
            }

            rigidbody.AddForce(transform.forward * (KPH / 400) * 10000);

            }
       
            else
            {
            fowardFriction = Ruedas[0].forwardFriction;
            sidewaysFriction = Ruedas[0].sidewaysFriction;

            fowardFriction.extremumValue = fowardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = 
                ((KPH * handBrakeFrictionMultiplier) / 300) + 1;

            for (int i = 0; i<4; i++)
            {
                Ruedas[i].forwardFriction = fowardFriction;
                Ruedas[i].sidewaysFriction = sidewaysFriction;

            }
        }

            for (int i = 2; i < 4; i++)
        {
            WheelHit wheelHit;

            Ruedas[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip < 0) driftFactor = (1 + -IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
            
            if(wheelHit.sidewaysSlip < 0) driftFactor = (1 + IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
        }
    }

    private float tempo;

    void ChequeaGiroRueda()
    {
        float blind = 0.28f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rigidbody.AddForce(transform.forward * -1500);

            if (IM.FrenoDeMano)
            {
                for (int i = 0; i<4; i++)
                {
                    WheelHit wheelHit;
                    Ruedas[i].GetGroundHit(out wheelHit);
                    if (wheelHit.sidewaysSlip > blind || wheelHit.sidewaysSlip < -blind)
                    {
                        //applyBooster(wheelHit.sidewaysSlip);
                    }
                }
            }
        }

        for (int i = 2; i < 4; i++){
            WheelHit wheelHit;
            Ruedas[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip < 0)
                tempo = (1 + -IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip * handBrakeFrictionMultiplier);
                if (tempo < 0.5) tempo = 0.5f;
            if (wheelHit.sidewaysSlip > 0)
                tempo = (1 + IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip * handBrakeFrictionMultiplier);
                if (tempo < 0.5) tempo = 0.5f;
            if (wheelHit.sidewaysSlip > .99f || wheelHit.sidewaysSlip < -.99f)
            {
                float velocity = 0;
                handBrakeFriction = Mathf.SmoothDamp(handBrakeFriction, tempo * 3, ref velocity, 0, 1f * Time.deltaTime);
            }

            else

                handBrakeFriction = tempo;


        }

    }

    private IEnumerator timedLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(.7f);
            radius = 6 + KPH / 20;
        }
    }
}

