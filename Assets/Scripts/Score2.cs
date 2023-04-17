using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        GameManager.gameManager.Score2();
    }
}
