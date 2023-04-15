using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    [SerializeField] private float hopForce; // The force applied to the hop
    [SerializeField] private float sightDistance;

    public GameObject rabbitObject;

    private Rigidbody rigidbody; // Reference to the Rigidbody component


    public bool hasEaten = false;
    private bool canReproduce = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
        Helpers.AddRabbit(this.transform);
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero)
        {
            Transform closestFox = Helpers.GetClosestFox(this.transform, sightDistance);
            Transform closestRabbit = Helpers.GetClosestRabbit(this.transform, sightDistance);
            Transform closestBush = Helpers.GetClosestBush(this.transform, sightDistance);
            if (closestFox != null)
            {
                Vector3 direction = this.transform.position - closestFox.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            } else if (closestBush != null && !hasEaten)
            {
                Vector3 direction = closestBush.transform.position - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            }
            else if (closestRabbit != null  && canReproduce)
            {
                Debug.Log("rabbits");

                Vector3 direction = closestRabbit.transform.position - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            }
            else
            {
                Debug.Log("Random");
                this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            }
            Hop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fox"))
        {
            Helpers.RemoveRabbit(this.transform);
            Destroy(this.gameObject);
        } 
        if (collision.transform.CompareTag("Rabbit") && canReproduce)
        {

            GameObject spawnedobject = Instantiate(rabbitObject, new Vector3(this.transform.position.x + 2f , this.transform.position.y, this.transform.position.z + 2f), this.transform.rotation);
            spawnedobject.GetComponent<RabbitController>().MakeRabbit(false);
            canReproduce = false;
        }
        if (collision.transform.CompareTag("Bush"))
        {
            hasEaten = true;
        }
    }

    public void MakeRabbit(bool canReproduce)
    {
        this.canReproduce = canReproduce;
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward + Vector3.up * 0.5f;
        Vector3 hopForceVector = direction * hopForce; 
        rigidbody.AddForce(hopForceVector, ForceMode.Impulse); 
    }
}