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

    public int indexRevisar;

    private void Awake()
    {
        dataCenter.SetActive(false);

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
