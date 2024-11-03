using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynoData : MonoBehaviour
{
    public ControlPosta carController; // Referencia al script que tiene los datos
    public int maxDataPoints = 100; // Número máximo de puntos en el gráfico

    private Queue<Vector2> horsepowerPoints; // Para almacenar los puntos de potencia
    private Queue<Vector2> wheelRPMPoints;   // Para almacenar los puntos de RPM de las ruedas
    private Queue<Vector2> totalEnergyPoints; // Para almacenar los puntos de energía total

    void Start()
    {
        horsepowerPoints = new Queue<Vector2>();
        wheelRPMPoints = new Queue<Vector2>();
        totalEnergyPoints = new Queue<Vector2>();
    }

    void Update()
    {
        // Obtener los datos del controlador del auto
        float currentPower = carController.engineRPM * -1;  // Método que devuelve la potencia del motor
        float currentWheelRPM = carController.wheelsRPM;  // Método que devuelve las RPM de las ruedas
        float currentEnergy = carController.TotalPower;  // Método que devuelve la energía total

        // Agregar los puntos al gráfico
        AddDataPoint(currentWheelRPM, currentPower, currentEnergy);
    }

    private void AddDataPoint(float rpm, float power, float energy)
    {
        if (horsepowerPoints.Count >= maxDataPoints)
        {
            horsepowerPoints.Dequeue();  // Remover el punto más antiguo
            wheelRPMPoints.Dequeue();
            totalEnergyPoints.Dequeue();
        }

        horsepowerPoints.Enqueue(new Vector2(rpm, power));
        wheelRPMPoints.Enqueue(new Vector2(rpm, power)); // Si necesitas puntos separados, ajusta esto
        totalEnergyPoints.Enqueue(new Vector2(rpm, energy));
    }

    public List<Vector2> GetHorsepowerPoints() => new List<Vector2>(horsepowerPoints);
    public List<Vector2> GetWheelRPMPoints() => new List<Vector2>(wheelRPMPoints);
    public List<Vector2> GetTotalEnergyPoints() => new List<Vector2>(totalEnergyPoints);
}
