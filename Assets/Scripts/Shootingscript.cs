using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingscript : MonoBehaviour
{

    [SerializeField] GameObject gun;
     GameObject bullet1;
     [SerializeField] Transform bulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
         
    }



    public void Shoot()
    {
        bullet1 = Instantiate(gameObject);
        bulletSpawn.GetComponent<Collider>();
        bullet1.transform.position = bulletSpawn.position;
        bullet1.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * -20, ForceMode.Impulse);
        Physics.IgnoreCollision(gun.GetComponent<Collider>(), bullet1.GetComponent<Collider>());
        
       
    }

}
