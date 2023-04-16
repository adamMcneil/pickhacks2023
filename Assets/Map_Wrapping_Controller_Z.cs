using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Wrapping_Controller_Z : MonoBehaviour
{
    public float z;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("here");
        if (collision.transform.CompareTag("Rabbit"))
        {
            collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, z);
        }
    }
}
