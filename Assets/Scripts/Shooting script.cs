using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingscript : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        bullet1.transform.position = gameObject.transform.position;
        bullet1 = Instantiate(bullet);
        bullet1.transform.position = bullet1.transform.position += new Vector3(0,0,1);
    }

}
