using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{

    public float vertical;
    public float horizontal;
    public bool FrenoDeMano;

    private void FixedUpdate()
    {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        FrenoDeMano = (Input.GetAxis("Jump") != 0)? true : false;

    }
}
