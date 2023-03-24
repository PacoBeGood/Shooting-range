using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingscript : MonoBehaviour
{

    [SerializeField] GameObject gun;
     GameObject bullet1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         bullet1.transform.position = bullet1.transform.localPosition += new Vector3(0,0,1);
    }

    public void Shoot()
    {
        bullet1 = Instantiate(gameObject);
        bullet1.transform.position = gun.transform.position;
        
       
    }

}
