using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int score;
    [SerializeField] private TMP_Text scoreText;
    public TMP_Text highscoreText;
    [SerializeField] private TMP_Text bulletsLeftText;
    public int highscore;
    [SerializeField] GameObject gun;
    GameObject bullet1;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;
    bool gunHeld;
    bool canShoot;
    [SerializeField] bool leftSide;
    [SerializeField] bool rightSide;
    [SerializeField] GameObject target;
    int bulletsLeft = 8;
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
        gunHeld = false;
        canShoot = true;
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        rightSide = true;
        leftSide = false;
        

    }


    void Update()
    {
        scoreText.text = "Score: " + score;
        highscoreText.text = "highscore: " + highscore;
        bulletsLeftText.text = "" + bulletsLeft; 

        if (rightSide == true)
        {
            target.gameObject.transform.position += new Vector3 (-0.1f,0,0);
        }
        if (leftSide == true)
        {
            target.gameObject.transform.position += new Vector3 (0.1f,0,0);
        }

    }
    public void LeftSideHit()
    {
        leftSide = true;
        rightSide = false;
    }
    public void RightSideHit()
    {
        rightSide = true;
        leftSide = false;
    }
    public void Score1()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;

        Debug.Log("score!");
        
    }
    public void Score2()

    {
        score = score + 2;
        scoreText.text = "Score: " + score;

        Debug.Log("score!");
    }
    public void Score3()
    {
        score = score + 3;
        scoreText.text = "Score: " + score;


    }
    public void Shoot()
    {
        if (gunHeld == true && canShoot == true)
        {
            bullet1 = Objectpool.SharedInstance.GetPooledObject();
            if (bullet1 != null)
            {
               // bulletSpawn.GetComponent<Collider>();

                bullet1.SetActive(true);
                bullet1.transform.position = bulletSpawn.position;

                bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * -120, ForceMode.Impulse);
                canShoot = false;
                StartCoroutine(WaitingTime());
               bulletsLeft =  bulletsLeft - 1;
               
            }

        }
    }
    public void GunInHand()
    {
        gunHeld = true;
    }
    public void GunOutOfHand()
    {
        gunHeld = false;
    }
    public void BulletInactive()
    {
        bullet1.SetActive(false);
    }


    public class PlayerData
    {
        public static PlayerData Instance;
        public int scoresave;
    }
    public void SaveGame()
    {
        PlayerData playerData = new PlayerData();
        playerData.scoresave = score;
        string json = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(Application.dataPath + "/playerData.json", json);
    }
    public void LoadGame()
    {
        string json = System.IO.File.ReadAllText(Application.dataPath + "/playerData.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        highscore = playerData.scoresave;

    }

    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
