using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    
    public Collider other;

    private void OnTriggerEnter(Collider other)
    {
        this.other = other;
        if (other.tag == "Bullet")
        {
            StartCoroutine(WaitAndRun());
        }

    }
    IEnumerator WaitAndRun()
    {
        yield return new WaitForSeconds(3);
        this.other.gameObject.SetActive(false);
    }

}
