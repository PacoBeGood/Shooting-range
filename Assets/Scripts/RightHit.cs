using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHit : MonoBehaviour
{
    
   void  OnTriggerEnter(Collider other)
    {
        if(other.name == "target") GameManager.gameManager.RightSideHit();
        
    }
}
