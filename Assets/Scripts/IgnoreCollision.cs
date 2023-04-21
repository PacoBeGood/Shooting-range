using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter (Collision gun)
    {
        if (gun.gameObject.tag == "Gun")
        {
            Physics.IgnoreCollision(gun.collider, GetComponent<Collider>());
        }
    }
}
