using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rabbit"))
        {
            Helpers.removeBushes(this.transform);
            Destroy(this.gameObject);
        } 
    }
}
