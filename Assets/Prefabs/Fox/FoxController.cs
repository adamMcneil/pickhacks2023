using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float hopForce = 5f;
    [SerializeField] private float sightDistance = 5f;
    private Rigidbody rigidbody;
    private List<Transform> rabbits;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero)
        {
            Transform closestRabbit = GetClosestRabbit();
            Debug.Log(closestRabbit);
            if (closestRabbit == null)
            {
                this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            }
            else
            {
                Vector3 direction = (this.transform.position - closestRabbit.transform.position).normalized;
                this.transform.rotation = Quaternion.Euler(0, Mathf.Sin(direction.x), 0);

            }
            Hop();
        }
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward + Vector3.up * 0.5f;
        Vector3 hopForceVector = direction * hopForce;
        rigidbody.AddForce(hopForceVector, ForceMode.Impulse);
    }

    private Transform GetClosestRabbit()
    {
        Transform closestRabbit = null;
        float distance = sightDistance * sightDistance;

        rabbits = Helpers.GetRabbits();
        foreach (var rabbit in rabbits)
        {
            Debug.Log(rabbit);
            Debug.Log((rabbit.transform.position - this.transform.position).sqrMagnitude);
            if ((rabbit.transform.position - this.transform.position).sqrMagnitude < distance)
            {
                closestRabbit = rabbit;
            }
        }
        return closestRabbit;
    }
}