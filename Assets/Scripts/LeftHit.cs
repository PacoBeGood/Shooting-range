using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHit : MonoBehaviour
{
    void  OnTriggerEnter(Collider other)
    {
        if (other.name == "target")
        { GameManager.gameManager.LeftSideHit();
         Debug.Log("hit");
        }
    }
}
