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
    [SerializeField] GameObject gun;
    GameObject bullet1;
    [SerializeField] Transform bulletSpawn;
    bool gunHeld;
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
    public void Shoot()
    {
        if (gunHeld == true)
        {
            bullet1 = Objectpool.SharedInstance.GetPooledObject();
            if (bullet1 != null)
            {
                bulletSpawn.GetComponent<Collider>();
                bullet1.SetActive(true);
                bullet1.transform.position = bulletSpawn.position;
                bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * -20, ForceMode.Impulse);
                Physics.IgnoreCollision(gun.GetComponent<Collider>(), bullet1.GetComponent<Collider>());

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
}
