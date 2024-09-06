using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RompiendoElVelocimetro : MonoBehaviour
{
    public Rigidbody rigid;
    public Transform imagen; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rigid.velocity.magnitude;
        imagen.eulerAngles = new Vector3(0, 0, speed * -4 + 150);
    }

}
