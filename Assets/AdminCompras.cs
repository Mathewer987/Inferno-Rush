using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AdminCompras : MonoBehaviour
{
    public GameObject PanelData;
    void Start()
    {
        PanelData.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cerrarPanel();
        }
    }

    public void turboManuela()
    {
        PanelData.SetActive(true);
    }

    public void cerrarPanel()
    {
        PanelData.SetActive(false);
    }

    public void vueltovich()
    {
        SceneManager.LoadScene("PlataformaSelectiva");
    }
}
