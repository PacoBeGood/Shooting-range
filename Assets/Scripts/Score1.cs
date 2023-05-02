using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score1 : MonoBehaviour
{
    void OnTriggerEnter()
    {
            GameManager.gameManager.Score1();
            Debug.Log("score1!");
            
        

    }
}
