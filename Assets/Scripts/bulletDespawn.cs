using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDespawn : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Bullet")
      {
GameManager.gameManager.BulletInactive();
      }
    
   }
}
