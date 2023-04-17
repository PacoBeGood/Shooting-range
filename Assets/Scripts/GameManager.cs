using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private int score;
    [SerializeField] private TMP_Text scoreText; 
    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
    void Start()
    {
        score = 0;
    }


    void Update()
    {
        scoreText.text = "Score: " + score;
    }
    public void Score1()
    {
     score = score + 1; 
    }
    public void Score2()
    {
     score = score + 2; 
    }
    public void Score3()
    {
     score = score + 3; 
    }
}
