using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopRandomDirection : MonoBehaviour
{
    public float hopForce = 5f; // The force applied to the hop

    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        if (rb.velocity == new Vector3(0,0,0))
        {
            this.transform.RotateAround(Vector3.up, Random.Range(0f, 360f));
            Hop();
        }
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward;
        direction.y = 1;
        Vector3 hopDirection = direction; // Generate a random direction vector with y set to 0
        Vector3 hopForceVector = hopDirection * hopForce; // Scale the direction vector with hop force
        rb.AddForce(hopForceVector, ForceMode.Impulse); // Apply the hop force as an impulse to the Rigidbody component
    }
}