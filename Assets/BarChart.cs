using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChart : MonoBehaviour
{
    private LineRenderer line;
    public Material material;
    private int currLines = 0;

    // Lista de puntos predefinidos (puedes definir las coordenadas en 3D)
    public List<Vector3> predefinedPoints = new List<Vector3>
    {
        new Vector3(0, 0, 10),    // Primer punto en Z = 10
        new Vector3(1, 2, 10),    // Segundo punto en Z = 10
        new Vector3(2, 3, 10),    // Tercer punto en Z = 10
        new Vector3(4, 5, 10)     // Cuarto punto en Z = 10
    };

    void Start()
    {
        DrawPredefinedPoints();
    }

    void DrawPredefinedPoints()
    {
        // Crear una nueva línea para los puntos predefinidos
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.material = material;
        line.positionCount = predefinedPoints.Count; // El número de puntos en la lista
        line.startWidth = 0.07f;
        line.endWidth = 0.07f;
        line.useWorldSpace = true;  // Usa espacio del mundo
        line.numCapVertices = 50;

        // Asignar los puntos predefinidos al LineRenderer
        for (int i = 0; i < predefinedPoints.Count; i++)
        {
            line.SetPosition(i, predefinedPoints[i]);
        }

        currLines++;
    }

}



