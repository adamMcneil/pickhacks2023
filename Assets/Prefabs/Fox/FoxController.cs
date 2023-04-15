using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public float hopForce = 5f;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero)
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            Hop();
        }
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward + Vector3.up * 0.5f;
        Vector3 hopForceVector = direction * hopForce;
        rigidbody.AddForce(hopForceVector, ForceMode.Impulse);
    }
}