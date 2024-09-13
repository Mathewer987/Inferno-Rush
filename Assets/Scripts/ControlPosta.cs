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


    public float maxSpeed = 40f;
    public GameManager manager;
    public bool reverse;
    public float handBrakeFrictionMultiplier = 2f;
    public float TotalPower;
    public float wheelsRPM;
    public AnimationCurve enginePower;
    [HideInInspector] public bool test; //engine sound boolean
    public bool primera;
    public float HD = 40f;
    public float engineRPM;
    public float smoothTime = 0.01f;
    public float[] gears;
    public int gearNum = 0;
    public float[] VCambios;
    public float Record;
    public bool YU = false;
    public bool cambiopolis;


    public float maxRPM, minRPM;
    [HideInInspector] public bool playPauseSmoke = false, hasFinished;

    private inputManager IM;
    public Autinhos A;

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
    private float lastValue;
    public int Loca = 0;
    bool ñ;
    float motorPower;



    public float[] slip = new float[4];

    void Start()
    {
        getObjects();
        maxSpeed = VCambios[Loca];

        for (int i = 0; i < 4; i++)
        {
            wheelMesh[i].transform.Rotate(0, 0, 90); // Ajuste inicial de la rotación
        }



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
        enanoBariloche();
        

        

    }

    private void CalcularPotencia()
    {
        // Calculate wheel RPM
        RPMRuedas();

        engineRPM = Mathf.Clamp(wheelsRPM * gears[gearNum], minRPM, maxRPM);

         motorPower = enginePower.Evaluate(engineRPM);

        TotalPower = motorPower * gears[gearNum] * IM.vertical ;

        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, Mathf.Clamp(1000 + (Mathf.Abs(wheelsRPM) * 3.6f * gears[gearNum]), minRPM, maxRPM), ref velocity, smoothTime);



    }

    private void Shifter()
    {



        if (!IsGrounded())
        {
            return;
        }

        if (gearChange == gearBox.Automatico)
        {
            if (engineRPM > maxRPM && gearNum < gears.Length - 1 && !reverse)
            {
                gearNum++;
                manager.changeGear();

            }
        }

        if (gearChange == gearBox.Manual)
        {
            if (Input.GetKeyDown(KeyCode.E))
                    
            {
                
                if ((KPH > HD - 1f && KPH < HD + 1f) && gearNum < gears.Length - 1)
                {
                    gearNum++;
                    manager.changeGear();
                    YU = true;
                    cambiopolis = true;
                    Cambialo();

                }    
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (gearNum > 0)
                {
                    gearNum--;
                    manager.changeGear();
                    cambiopolis = true;
                    CambialoMenos();


                }
            }


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

    public float maxWheelRPM = 600; // Define el límite máximo de RPM para las ruedas

    private void RPMRuedas()
    {
        float sum = 0;
        int R = 0;

        for (int i = 0; i < 4; i++)
        {
            sum += Ruedas[i].rpm;
            R++;
        }

        // Calcula el promedio de las RPM de las ruedas
        wheelsRPM = (R != 0) ? sum / R : 0;

        // Limita las RPM de las ruedas al máximo definido
        wheelsRPM = Mathf.Clamp(wheelsRPM, -maxWheelRPM, maxWheelRPM);

        // Gestiona el cambio de sentido (reversa)
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

        if (KPH > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * (maxSpeed / 3.6f);
        }

        if (IM.FrenoDeMano)
        {
            Ruedas[3].brakeTorque = Ruedas[2].brakeTorque = 1000000;
        }

        else
        {
            Ruedas[3].brakeTorque = Ruedas[2].brakeTorque = 0;

        }

        if (IM.boosting)
        {
            rigidbody.AddForce(Vector3.forward * thrust);
        }

        if (KPH == -maxSpeed)
        {
            Debug.Log("Velocidad: " + KPH + "RPMRuedas: " + wheelsRPM);
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

            // Si quieres que la rotación Z inicial sea 90 grados
            Quaternion rotacionAdicional = Quaternion.Euler(0, 0, 90);

            wheelMesh[i].transform.position = PosicionRueda;
            wheelMesh[i].transform.rotation = RotacionRueda * rotacionAdicional; // Aplicar la rotación adicional
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

    private void ajustarTraccion()
    {
        //tine it takes to go from normal drive to drift 
        float driftSmothFactor = .7f * Time.deltaTime;

        if (IM.FrenoDeMano)
        {
            sidewaysFriction = Ruedas[0].sidewaysFriction;
            fowardFriction = Ruedas[0].forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = fowardFriction.extremumValue = fowardFriction.asymptoteValue =
                Mathf.SmoothDamp(fowardFriction.asymptoteValue, driftFactor * handBrakeFrictionMultiplier, ref velocity, driftSmothFactor);

            for (int i = 0; i < 4; i++)
            {
                Ruedas[i].sidewaysFriction = sidewaysFriction;
                Ruedas[i].forwardFriction = fowardFriction;
            }

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = fowardFriction.extremumValue = fowardFriction.asymptoteValue = 1.1f;
            //extra grip for the front wheels
            for (int i = 0; i < 2; i++)
            {
                Ruedas[i].sidewaysFriction = sidewaysFriction;
                Ruedas[i].forwardFriction = fowardFriction;
            }
            rigidbody.AddForce(transform.forward * (KPH / 400) * 10000);
        }
        //executed when FrenoDeMano is being held
        else
        {

            fowardFriction = Ruedas[0].forwardFriction;
            sidewaysFriction = Ruedas[0].sidewaysFriction;

            fowardFriction.extremumValue = fowardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue =
                ((KPH * handBrakeFrictionMultiplier) / 300) + 1;

            for (int i = 0; i < 4; i++)
            {
                Ruedas[i].forwardFriction = fowardFriction;
                Ruedas[i].sidewaysFriction = sidewaysFriction;

            }
        }

        //checks the amount of slip to control the drift
        for (int i = 2; i < 4; i++)
        {

            WheelHit wheelHit;

            Ruedas[i].GetGroundHit(out wheelHit);
            //smoke
            if (wheelHit.sidewaysSlip >= 0.3f || wheelHit.sidewaysSlip <= -0.3f || wheelHit.forwardSlip >= .3f || wheelHit.forwardSlip <= -0.3f)
                playPauseSmoke = true;
            else
                playPauseSmoke = false;


            if (wheelHit.sidewaysSlip < 0) driftFactor = (1 + -IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);

            if (wheelHit.sidewaysSlip > 0) driftFactor = (1 + IM.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
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

    private void enanoBariloche()
    {
        if (TotalPower == 0)
        {
            Ruedas[3].brakeTorque = Ruedas[2].brakeTorque = fuerzaDeFreno / 2;
        }

        else
        {
            Ruedas[3].brakeTorque = Ruedas[2].brakeTorque = 0;
        }
    }

   private void Cambialo()
    {
        if (Loca < VCambios.Length) 
        {
            Loca = Loca + 1;
            maxSpeed = VCambios[Loca];
            HD = maxSpeed;
            
        }
    }


  

    private void CambialoMenos()
    {
        if (Loca > 0)
        {
            float smoothTime = 0.0f;
            float velocity = 0.0f;
            Loca = Loca - 1;
            maxSpeed = Mathf.SmoothDamp(maxSpeed, VCambios[Loca], ref velocity, smoothTime);
            //maxSpeed = VCambios[Loca];
            HD = maxSpeed;
        }
    }

    
}

