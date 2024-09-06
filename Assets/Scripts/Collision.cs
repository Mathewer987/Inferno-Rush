using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update

    public bool ColuTermi;
    public Misiones RR;
    private void OnTriggerEnter(Collider other)
    {
       

        
        if (other.CompareTag("Hitbox"))
        {
                ColuTermi = true;
            
        }
    }

}
