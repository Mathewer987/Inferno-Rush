using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynoGraph : MonoBehaviour
{
    public DynoData dynoData;
    public LineRenderer horsepowerLine;
    public LineRenderer wheelRPMLine;
    public LineRenderer totalEnergyLine; // Nuevo LineRenderer para la energía total
    public float graphWidth = 10f;
    public float graphHeight = 5f;
    public ControlPosta RR; // Referencia al script que tiene los datos

    void Update()
    {
        List<Vector2> horsepowerPoints = dynoData.GetHorsepowerPoints();
        List<Vector2> wheelRPMPoints = dynoData.GetWheelRPMPoints();
        List<Vector2> totalEnergyPoints = dynoData.GetTotalEnergyPoints();

        // Dibujar la línea de horsepower
        horsepowerLine.positionCount = horsepowerPoints.Count;
        for (int i = 0; i < horsepowerPoints.Count; i++)
        {
            float x = (horsepowerPoints[i].x / dynoData.maxDataPoints) * graphWidth;
            float y = horsepowerPoints[i].y / RR.TotalPower * graphHeight; // Normaliza por la potencia máxima
            horsepowerLine.SetPosition(i, new Vector3(x, y, 0));
        }

        // Dibujar la línea de wheel RPM
        wheelRPMLine.positionCount = wheelRPMPoints.Count;
        for (int i = 0; i < wheelRPMPoints.Count; i++)
        {
            float x = (wheelRPMPoints[i].x / dynoData.maxDataPoints) * graphWidth;
            float y = wheelRPMPoints[i].y / RR.TotalPower * graphHeight; // Puede ser ajustado
            wheelRPMLine.SetPosition(i, new Vector3(x, y, 0));
        }

        // Dibujar la línea de energía total
        totalEnergyLine.positionCount = totalEnergyPoints.Count;
        for (int i = 0; i < totalEnergyPoints.Count; i++)
        {
            float x = (totalEnergyPoints[i].x / dynoData.maxDataPoints) * graphWidth;
            float y = totalEnergyPoints[i].y / RR.TotalPower * graphHeight; // Normaliza por energía máxima
            totalEnergyLine.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
