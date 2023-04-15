using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public float hopForce = 5f; // The force applied to the hop

    private Rigidbody rigidbody; // Reference to the Rigidbody component

    public static List<Transform> rabbits = null;

    void Start()
    {
        if (rabbits == null)
        {
            rabbits = new List<Transform>();
            rabbits.Add(this.transform);
        }
        else
        {
            rabbits.Add(this.transform);
        }
        rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
        Helpers.AddRabbit(this.transform);
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero)
        {
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            Hop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fox"))
        {
            rabbits.Remove(collision.transform);
            Helpers.RemoveRabbit(this.transform);
            Destroy(this.gameObject);
        }
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward + Vector3.up * 0.5f;
        Vector3 hopForceVector = direction * hopForce; 
        rigidbody.AddForce(hopForceVector, ForceMode.Impulse); 
    }
}