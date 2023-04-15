using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float hopForce;
    [SerializeField] private float sightDistance;
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
            //Debug.DrawRay(Vector3.zero, closestRabbit.position, Color.red, 1f);
            //Debug.DrawRay(Vector3.zero, this.transform.position, Color.red, 1f);
            if (closestRabbit == null)
            {
                this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            }
            else
            {
                Vector3 direction = closestRabbit.transform.position - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                //Debug.DrawRay(Vector3.zero, Vector3.right, Color.red, 1f);
                //Debug.DrawRay(Vector3.zero, normilizedDirection, Color.red, 1f);
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);

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
            if ((rabbit.position - this.transform.position).sqrMagnitude < distance)
            {
                closestRabbit = rabbit;
            }
        }
        return closestRabbit;
    }
}