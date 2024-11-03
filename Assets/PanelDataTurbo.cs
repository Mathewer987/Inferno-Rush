using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelDataTurbo : MonoBehaviour
{
    public List<Button> miniDatas;
    public string[] nom;
    public int[] PSIs;
    public int[] Pesosim;
    public List<Color> colors;

    public GameObject dataCenter;
    public Text title;
    public Text HP;
    public Text Peso;
    public Text Estado;
    public string[] turboKeys;
    public int seleccion;

    public int PSI;
    public int PSO;

    public int indexRevisar;
    private void Awake()
    {
        dataCenter.SetActive(false);

        //DeleteTurboPreferences();


        // Cargar la selección guardada (si existe)
        if (PlayerPrefs.HasKey("SeleccionTurbo"))
        {
            // Obtener el índice del botón seleccionado guardado
            seleccion = PlayerPrefs.GetInt("SeleccionTurbo");

            // Aplicar el color verde al botón guardado
            Button selectedButton = miniDatas[seleccion];
            ChangeButtonColor(selectedButton, Color.green);
            PSI = PlayerPrefs.GetInt("PSIss");
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
                HP.text = "PSI: " + PSIs[indexRevisar].ToString();
                Peso.text = "Peso: " + Pesosim[indexRevisar].ToString();
                title.color = colors[indexRevisar];  // Cambia el color del texto (las letras del título)
                if (PlayerPrefs.HasKey(turboKeys[indexRevisar]))
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
        // Obtiene el índice del botón presionado
        int buttonIndex = miniDatas.IndexOf(clickedButton);

        // Verifica si el motor está "owned"
        if (PlayerPrefs.HasKey(turboKeys[buttonIndex]))
        {
            // Restablece el color de todos los botones antes de cambiar el color del botón presionado
            foreach (Button btn in miniDatas)
            {
                ResetButtonColor(btn); // Restablece el color de los otros botones
            }

            Debug.Log("Botón presionado: " + buttonIndex + " - " + nom[buttonIndex]);

            seleccion = buttonIndex;

            // Guardar la selección en PlayerPrefs
            PlayerPrefs.SetInt("SeleccionTurbo", seleccion);
            PlayerPrefs.SetInt("PSIss", PSIs[buttonIndex]); // Guardar caballos de fuerza
            PlayerPrefs.SetInt("PesoTurbo", Pesosim[buttonIndex]); // Guardar peso
            PlayerPrefs.Save(); // Asegura que los datos se guarden

            PSI = PlayerPrefs.GetInt("PSIss"); // Guardar caballos de fuerza
            PSO = PlayerPrefs.GetInt("PesoTurbo"); // Cargar peso

            // Cambiar el color del botón presionado
            ChangeButtonColor(clickedButton, Color.green);
            // Forzar la actualización visual del botón
            ForceButtonUpdate(clickedButton);
        }
        else
        {
            // Muestra un mensaje si el motor no es "owned"
            Debug.Log("Este turbo no está disponible para selección porque no lo posees.");
            // Aquí puedes añadir un mensaje en la UI si lo deseas
        }
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


    public void BuyTurbo(int turboIndex)
    {

        turboIndex = indexRevisar;
        // Verificar si el motor no está ya "owned"
        if (PlayerPrefs.GetString(turboKeys[turboIndex]) != "owned")
        {
            // Aquí podrías agregar la lógica para verificar monedas o recursos
            // Por simplicidad, asumimos que siempre se puede comprar
            PlayerPrefs.SetString(turboKeys[turboIndex], "owned");

            Debug.Log("Turbo " + turboKeys[turboIndex] + " ahora es tuyo!");

            // Actualiza la información del motor o UI
        }
        else
        {
            Debug.Log("Ya posees este turbo!");
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

    public void DeleteTurboPreferences()
    {
        foreach (string turboKey in turboKeys)
        {
            if (PlayerPrefs.HasKey(turboKey))
            {
                PlayerPrefs.DeleteKey(turboKey);
            }
        }

        PlayerPrefs.Save(); // Guarda los cambios
        Debug.Log("Preferencias de turbos eliminadas!");
    }
}
