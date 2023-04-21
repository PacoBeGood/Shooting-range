using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingscript : MonoBehaviour
{

    [SerializeField] GameObject gun;
     GameObject bullet1;
     [SerializeField] Transform bulletSpawn;
     bool gunHeld;
    // Start is called before the first frame update
    void Start()
    {
        gunHeld = false;
    }
    

    // Update is called once per frame
    public void GunInHand()
    {
        gunHeld = true;
    }
    public void GunOutOfHand()
    {
        gunHeld = false;
    }
    public void Shoot()
    {
        if (gunHeld == true)
        {
            bullet1 = Objectpool.SharedInstance.GetPooledObject();
            if (bullet1 != null)
            {
                bulletSpawn.GetComponent<Collider>();
        bullet1.transform.position = bulletSpawn.position;
        bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * -20, ForceMode.Impulse);
        Physics.IgnoreCollision(gun.GetComponent<Collider>(), bullet1.GetComponent<Collider>());
            }
        
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            bullet1.SetActive(false);
        }
    }

}
