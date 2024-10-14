using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    private WheelFrictionCurve forwardFriction, sidewaysFriction;

    public ParticleSystem[] nitrusSmoke;
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
    float variableAcumulativa = 0f; // Inicialízala

    public float engineRPM;
    public float smoothTime = 0.01f;
    public float[] gears;
    public int gearNum = 0;
    public float[] VCambios;
    public float Record;
    public bool YU = false;
    public bool cambiopolis;
    public float previa;
    public bool acelerando;
    public bool desacelera2;
    bool pini;
    public float MV;
    public bool CalentonJ;
    public ParticleSystem[] smoke;
    public bool gtr;
    private bool paloma = false;
    public float valorRC;



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
    private bool flag = false;
    private float lastValue;
    public int Loca = 0;
    public bool yupi;




    public float[] slip = new float[4];

    public Vector3 nuevaPosicion = new Vector3(300f, 21f, 340.54f);
    public Quaternion nuevaRotacion = Quaternion.Euler(0f, -280f, 0f);
    public string nombreEscenaObjetivo = "Prueba Manejo"; // Nombre de la escena objetivo

    private void Awake()
    {
       
        

    }

    void Start()
    {
        getObjects();
        maxSpeed = VCambios[Loca];



    }

    private void Update()
    {
        Shifter();
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "PlataformaSelectiva") return;
        AgregarFuerzaAbajo();
        AnimacionRuedas();
      
            Movela();

        
        Rotala();
        DameFriccion();
        CalcularPotencia();
        ajustarTraccion();
        enanoBariloche();
        Desacelera2();
        ActivaNitruvish();
        MV = IM.vertical;


        if (CalentonJ == false && IsGrounded())
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;

            // Congela la rotación en los tres ejes
            rigidbody.constraints |= RigidbodyConstraints.FreezeRotation;
        }

        else
        {
            rigidbody.constraints = RigidbodyConstraints.None;

        }

    }

    private IEnumerator FAFA()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);


        }
    }


    private void CalcularPotencia()
    {
        // Calculate wheel RPM
        RPMRuedas();

        engineRPM = Mathf.Clamp(wheelsRPM * gears[gearNum], minRPM, maxRPM);

        float motorPower = enginePower.Evaluate(engineRPM);

        TotalPower = motorPower * gears[gearNum] * IM.vertical * -1 * motorTorque;

        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, Mathf.Clamp(1000 + (Mathf.Abs(wheelsRPM) * 3.6f * gears[gearNum]), minRPM, maxRPM), ref velocity, smoothTime);



    }

    private void Shifter()
    {

        if (reverse == true)
        {
            gearNum = 0;
        }

        if (!IsGrounded())
        {
            return;
        }

        if (gearChange == gearBox.Automatico)
        {
            if ((KPH < HD + 0.5f && KPH > HD - 0.5f) && !reverse && gearNum < gears.Length - 1 && acelerando == true)
            {
                if(gearNum < 4){
                    gearNum++;
                    manager.changeGear();
                    YU = true;
                    cambiopolis = true;
                    Cambialo();
                }
                
            }

            else if (!reverse && desacelera2 == true)
            {

            foreach (float velocidad in VCambios){
              
                    if ((KPH < velocidad + 0.4f && KPH > velocidad - 0.4f) && pini == false && gearNum > 0)
                    {
                        if(gearNum > 0)
                        gearNum--;
                        manager.changeGear();
                        cambiopolis = true;
                        CambialoMenos();
                        Debug.Log("sad");
                        pini = true;
                        StartCoroutine(PONO());

                    }

                }
                
            }


        }

        if (gearChange == gearBox.Manual)
        {
            if (Input.GetKeyDown(KeyCode.E) && !reverse)
                    
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

            else if (Input.GetKeyDown(KeyCode.Q) && !reverse)
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
            // Aplicar fuerza de freno en las ruedas traseras
            Ruedas[2].brakeTorque = fuerzaDeFreno;
            Ruedas[3].brakeTorque = fuerzaDeFreno;
        }
        else
        {
            // Quitar la fuerza de freno
            Ruedas[2].brakeTorque = 0;
            Ruedas[3].brakeTorque = 0;
        }


        if (IM.boosting)
        {
            rigidbody.AddForce(Vector3.right * thrust);
        }

        if (KPH == -maxSpeed)
        {
            Debug.Log("Velocidad: " + KPH + "RPMRuedas: " + wheelsRPM);
        }

        if (CalentonJ == false && TotalPower * -1 > 0 )
        {
            for (int i = 0; i < smoke.Length; i++)
            {
                var emission = smoke[i].emission;
                var emmision = smoke[i].emission;
                emmision.rateOverTime = ((int)wheelsRPM * -1 <= 2000) ? Mathf.Clamp((int)wheelsRPM * -1 / 16.8f, 0, 2000) : 2000;
  
                smoke[i].Play();

                if (valorRC <= 35)
                {
                    valorRC += Time.deltaTime * 2.5f;
                }
                valorRC = Mathf.Clamp(valorRC, 0, 100);
                

                if (valorRC >= 35 && TotalPower != 0 && paloma == false)
                {
                    StartCoroutine(LOL());
                }




            }

            

        }

        if (valorRC >= 35 && TotalPower == 0 && CalentonJ == false)
        {
            CalentonJ = true;
        }









    }

    private IEnumerator LOL()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);

            if (TotalPower != 0)
            {
                Debug.Log("1.5");
                paloma = true;
                yupi = true;

            }
        }
    }


    private IEnumerator UNseg()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                
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


    private float driftFactor;

    private void ajustarTraccion()
    {
        float driftSmothFactor = .7f * Time.deltaTime;

        if (IM.FrenoDeMano)
        {
            // Ajustar la fricción lateral (sideways) solo para las ruedas traseras
            for (int i = 2; i < 4; i++) // Solo ruedas traseras
            {
                sidewaysFriction = Ruedas[i].sidewaysFriction;
                forwardFriction = Ruedas[i].forwardFriction;

                // Reducir la fricción lateral para permitir el drift
                float velocity = 0;
                sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue =
                    Mathf.SmoothDamp(sidewaysFriction.extremumValue, driftFactor * handBrakeFrictionMultiplier, ref velocity, driftSmothFactor);

                // No tocar la fricción longitudinal (forwardFriction) o mantenerla baja para evitar que se frene
                forwardFriction.extremumValue = forwardFriction.asymptoteValue = 1.2f; // No poner esto en valores muy bajos

                Ruedas[i].sidewaysFriction = sidewaysFriction;
                Ruedas[i].forwardFriction = forwardFriction;
            }

            // Dar más agarre a las ruedas delanteras para mantener el control
            sidewaysFriction = Ruedas[0].sidewaysFriction;
            forwardFriction = Ruedas[0].forwardFriction;

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = 1.5f;
            forwardFriction.extremumValue = forwardFriction.asymptoteValue = 1.5f;

            for (int i = 0; i < 2; i++) // Solo ruedas delanteras
            {
                Ruedas[i].sidewaysFriction = sidewaysFriction;
                Ruedas[i].forwardFriction = forwardFriction;
            }

            // Añadir fuerza para mantener el deslizamiento en lugar de frenar
            rigidbody.AddForce(transform.forward * (KPH / 400) * 40000);
        }
        else
        {
            // Si no se está derrapando, ajustar la fricción para conducción normal
            forwardFriction = Ruedas[0].forwardFriction;
            sidewaysFriction = Ruedas[0].sidewaysFriction;

            forwardFriction.extremumValue = forwardFriction.asymptoteValue =
            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = ((KPH * handBrakeFrictionMultiplier) / 300) + 1;

            for (int i = 0; i < 4; i++)
            {
                Ruedas[i].forwardFriction = forwardFriction;
                Ruedas[i].sidewaysFriction = sidewaysFriction;
            }
        }

        // Calcular el drift factor basado en el deslizamiento lateral (sidewaysSlip)
        for (int i = 2; i < 4; i++) // Sólo ruedas traseras
        {
            WheelHit wheelHit;
            Ruedas[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip >= 0.3f || wheelHit.sidewaysSlip <= -0.3f || wheelHit.forwardSlip >= 0.3f || wheelHit.forwardSlip <= -0.3f)

                playPauseSmoke = true;
            else
                playPauseSmoke = false;

            if (wheelHit.sidewaysSlip < 0)
                driftFactor = Mathf.Abs(wheelHit.sidewaysSlip); // Invertir para corregir signo
            else if (wheelHit.sidewaysSlip > 0)
                driftFactor = Mathf.Abs(wheelHit.sidewaysSlip);
        }
    }

    [HideInInspector] public bool playPauseSmoke = false;

    private IEnumerator timedLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(.7f);
            radius = 6 + KPH / 20;

        }
    }

    private IEnumerator tiempoDesacelera2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            previa = KPH;

        }
    }
    private IEnumerator PONO()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            pini = false;

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

    private void Desacelera2()
    {
        float previousSpeed = rigidbody.velocity.magnitude;
        float nueva = KPH;

        StartCoroutine(tiempoDesacelera2());

        if (KPH < previa)
        {
            desacelera2 = true;
            acelerando = false;
        }
        else if (KPH > previa)
        {
            desacelera2 = false;
            acelerando = true;
        }
    }

    public void ActivaNitruvish()
    {
        if (!IM.boosting && nitrusValue <= 10)
        {
            nitrusValue += Time.deltaTime / 1.5f;
        }
        else
        {
            nitrusValue -= (nitrusValue <= 0) ? 0 : Time.deltaTime / 0.5f;

        }

        if (IM.boosting) {
            if (nitrusValue > 0) ArrancaNitroEmisor();
            else paraNitroEmisor();
            
        }

        else paraNitroEmisor();

    }

    public void ArrancaNitroEmisor()
    {
        if (nitrusFlag) return;

        for(int i = 0; i< nitrusSmoke.Length; i++)
        {
            nitrusSmoke[i].Play();
        }
        rigidbody.AddForce(transform.forward * 10000);
        nitrusFlag = true;
    }

    public float nitrusValue;
    public bool nitrusFlag;

    public void paraNitroEmisor()
    {
        if (!nitrusFlag) return;

        for (int i = 0; i < nitrusSmoke.Length; i++)
        {
            nitrusSmoke[i].Stop();
        }
        rigidbody.AddForce(transform.forward * 10000);
        nitrusFlag = false;
    }

}

