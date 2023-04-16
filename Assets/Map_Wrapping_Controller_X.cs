using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Wrapping_Controller_X : MonoBehaviour
{
    public float x;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rabbit"))
        {
            collision.transform.position = new Vector3(x, collision.transform.position.y, collision.transform.position.z);
        }
    }
}
