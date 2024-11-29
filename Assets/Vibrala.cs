using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrala : MonoBehaviour
{
    public Camera mainCamera;
    public bool asdf;
    public ControlPosta RR;
    public bool ghj;
    public Misiones MM;
    // El valor que determina la magnitud de la sacudida
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = mainCamera.transform.localPosition; // Guardamos la posición original de la cámara
        float elapsed = 0.0f;

        // Mientras la duración no haya terminado
        while (elapsed < duration)
        {
            // Generamos valores aleatorios para la sacudida en el eje X y Y
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Movemos la cámara por una distancia aleatoria dentro de la magnitud
            mainCamera.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            // Esperamos hasta el siguiente frame
            yield return null;
        }

        // Restauramos la posición original de la cámara
        mainCamera.transform.localPosition = originalPosition;
    }

    // Método para activar el Screen Shake
    public void TriggerShake(float duration, float magnitude)
    {
        asdf = true;
        StartCoroutine(Shake(duration, magnitude));
        asdf = false;

    }
    private void FixedUpdate()
    {
        if (RR.CalentonJ == true && (MM.Ganaste == false && MM.nein == false))
        {
            if (ghj == false)
            {
                ghj = true;
                StartCoroutine(Tiempo());
            }

        }

    }

    public IEnumerator Tiempo()
    {
        // Espera por el tiempo especificado
        yield return new WaitForSeconds(30f);
        ghj = false;

        TriggerShake(0.4f, 0.3f); // 0.5 segundos de duración y magnitud de 0.2
    }
}
