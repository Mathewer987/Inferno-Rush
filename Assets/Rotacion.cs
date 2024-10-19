using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public GameObject Primera;
    public GameObject Segunda;


    public float rpm; // Valor de las RPM del auto
    public float maxRPM; // Valor máximo de RPM
    public ControlPosta RR;
    void Update()
    {
        rpm = RR.wheelsRPM * -1;
        maxRPM = RR.maxWheelRPM;

        // Normaliza el valor de las RPM (0 a maxRPM) en un valor entre 0 y 1
        float porcentajeRPM = Mathf.Clamp01(rpm / maxRPM);

        // Calcula la velocidad de rotación en base a las RPM
        float velocidadRotacion = porcentajeRPM * 360f; // 360 grados por revolución (puedes ajustar este valor)

        // Aplica la rotación acumulativa en el eje Z, multiplicada por el tiempo para que sea suave
        Primera.transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
        Segunda.transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }
}
