using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sphere(Clone)")
        {
            Debug.Log("score");
        GameManager.gameManager.Score1();
        GameManager.gameManager.BulletInactive();
        }
        
        
    }
}
