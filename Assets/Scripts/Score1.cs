using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GameManager.gameManager.Score1();
            GameManager.gameManager.BulletInactive();
        }

    }
}
