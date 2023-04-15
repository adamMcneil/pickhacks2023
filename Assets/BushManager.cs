using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushManager : MonoBehaviour
{
    private void Start()
    {
        Helpers.AddBushes(this.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rabbit"))
        {
            Helpers.RemoveBushes(this.transform);
            Destroy(this.gameObject);
        } 
    }
}
