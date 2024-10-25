using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PanelDataEngine : MonoBehaviour
{
    public List<Button> miniDatas;
    public string[] nom;
    public int[] HPs;
    public int[] Pesosim;

    public GameObject dataCenter;
    public Text title;
    public Text HP;
    public Text Peso;
    public Text Estado;
    public string[] motorKeys;
    public int seleccion;

    public int indexRevisar;

    private void Awake()
    {
        dataCenter.SetActive(false);

        // Cargar la selección guardada (si existe)
        if (PlayerPrefs.HasKey("SeleccionMotor"))
        {
            // Obtener el índice del botón seleccionado guardado
            seleccion = PlayerPrefs.GetInt("SeleccionMotor");

            // Aplicar el color verde al botón guardado
            Button selectedButton = miniDatas[seleccion];
            ChangeButtonColor(selectedButton, Color.green);
        }
    }





    private void FixedUpdate()
    {

        bool isHovering = false; // Bandera para verificar si estamos sobre algún botón

        foreach (Button infos in miniDatas)
        {
            if (IsPointerOverUIObject(infos.gameObject))
            {
                indexRevisar = miniDatas.IndexOf(infos);
                title.text = nom[indexRevisar];
                HP.text = "Horse Power: " + HPs[indexRevisar].ToString();
                Peso.text = "Peso: " + Pesosim[indexRevisar].ToString();

                // Comprobar si el motor es "owned" y mostrar el estado
                if (PlayerPrefs.HasKey(motorKeys[indexRevisar]))
                {
                    Estado.text = "Status: Owned"; // Mostrar que el motor es propiedad
                }
                else
                {
                    Estado.text = "Status: Not Owned"; // Mostrar que el motor no es propiedad
                }

                dataCenter.SetActive(true);  
                isHovering = true; 
                break;
            }
        }

        // Si no estamos sobre ningún botón, ocultamos el panel
        if (!isHovering)
        {
            dataCenter.SetActive(false);
        }
    }


    public void SeleccionArgentina(Button clickedButton)
    {
        // Restablece el color de todos los botones antes de cambiar el color del botón presionado
        foreach (Button btn in miniDatas)
        {
            ResetButtonColor(btn); // Restablece el color de los otros botones
        }

        // Obtiene el índice del botón presionado
        int buttonIndex = miniDatas.IndexOf(clickedButton);
        Debug.Log("Botón presionado: " + buttonIndex + " - " + nom[buttonIndex]);

        seleccion = buttonIndex;

        // Guardar la selección en PlayerPrefs
        PlayerPrefs.SetInt("SeleccionMotor", seleccion);
        PlayerPrefs.Save(); // Asegura que los datos se guarden

        // Cambiar el color del botón presionado
        ChangeButtonColor(clickedButton, Color.green);

        // Forzar la actualización visual del botón
        ForceButtonUpdate(clickedButton);
    }


    // Método para cambiar el color del botón
    private void ChangeButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color; // Cambia el color en el estado normal
        button.colors = cb; // Asigna el ColorBlock modificado al botón
    }

    // Método para restablecer el color del botón al predeterminado
    private void ResetButtonColor(Button button)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = Color.white; // Cambia a tu color predeterminado (blanco o el que desees)
        button.colors = cb;
    }

    // Método para forzar la actualización visual del botón
    private void ForceButtonUpdate(Button button)
    {
        // Desactivar y reactivar el botón para forzar que Unity lo redibuje con el nuevo ColorBlock
        button.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
    }


    public void BuyMotor(int motorIndex)
    {
        // Verificar si el motor no está ya "owned"
        if (PlayerPrefs.GetString(motorKeys[motorIndex]) != "owned")
        {
            // Aquí podrías agregar la lógica para verificar monedas o recursos
            // Por simplicidad, asumimos que siempre se puede comprar
            PlayerPrefs.SetString(motorKeys[motorIndex], "owned");

            Debug.Log("Motor " + motorKeys[motorIndex] + " ahora es tuyo!");

            // Actualiza la información del motor o UI
        }
        else
        {
            Debug.Log("Ya posees este motor!");
        }
    }

    private bool IsPointerOverUIObject(GameObject target)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject == target)
            {
                return true; // El puntero está sobre el objeto UI
            }
        }

        return false;
    }

}
