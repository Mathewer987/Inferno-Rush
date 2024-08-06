using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RompiendoElVelocimetro : MonoBehaviour
{
    public Rigidbody rigid;
    public Image imagen; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rigid.velocity.magnitude;
        imagen.transform.eulerAngles = new Vector3(0, 0, speed * -5 + 150);
    }
}
