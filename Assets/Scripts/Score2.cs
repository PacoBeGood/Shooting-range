using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GameManager.gameManager.Score2();
            GameManager.gameManager.BulletInactive();
        }

    }
}
