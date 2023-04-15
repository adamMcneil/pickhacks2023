using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    [SerializeField] private float hopForce; // The force applied to the hop
    [SerializeField] private float sightDistance;

    public GameObject rabbitObject;

    private Rigidbody rigidbody; // Reference to the Rigidbody component

    private float lifeTime = 60;
    public float hungerTime = 10;
    public float thristTime = 10;
    public float reproductionTime = 10;
    public bool hasEaten = false;
    private bool canReproduce = true;
    private float reproductionCoolDown = 10f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
        Helpers.AddRabbit(this.transform);
        StartCoroutine(LifeTimer());
        StartCoroutine(TickTimer());
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero)
        {
            CalculateRotation();
            Hop();
        }
    }

    private void CalculateRotation()
    {
        Transform closestFox = Helpers.GetClosestFox(this.transform, sightDistance);
        Transform closestRabbit = Helpers.GetClosestRabbit(this.transform, sightDistance);
        Transform closestBush = Helpers.GetClosestBush(this.transform, sightDistance);
        Vector3 closestWater = Helpers.GetClosestWater(this.transform, sightDistance);
        if (closestFox != null)
        {
            Vector3 direction = this.transform.position - closestFox.transform.position;
            Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
            this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
        }
        else if (hungerTime < thristTime && hungerTime < reproductionTime)
        {
            if(closestRabbit != null)
            {
                Vector3 direction = closestBush.transform.position - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            }
        }
        else if(reproductionTime < thristTime)
        {
            if (closestRabbit != null)
            {
                Vector3 direction = closestRabbit.transform.position - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            }
        }
        else
        {
            if (closestWater != Vector3.zero)
            {
                Vector3 direction = closestWater - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            }
        }
        // Debug.Log("Random");
        // this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0)
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fox"))
        {
            Helpers.RemoveRabbit(this.transform);
            Destroy(this.gameObject);
        } 
        if (collision.transform.CompareTag("Rabbit") && canReproduce && hungerTime > 5)
        {
            GameObject spawnedobject = Instantiate(rabbitObject, new Vector3(this.transform.position.x + 2f , this.transform.position.y, this.transform.position.z + 2f), this.transform.rotation);
            spawnedobject.GetComponent<RabbitController>().MakeRabbit(false);
            OnReproduce();
        }
        if (collision.transform.CompareTag("Bush"))
        {
            hasEaten = true;
        }
    }

    public void OnDeath()
    {
        Helpers.RemoveRabbit(this.transform);
        Destroy(this.gameObject);
    }

    public void MakeRabbit(bool canReproduce)
    {
        this.canReproduce = canReproduce;
        StartCoroutine(ReproductionTimer());
    }
    public void OnReproduce()
    {
        canReproduce = false;
        hasEaten= false;
        StartCoroutine(ReproductionTimer());
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward + Vector3.up * 0.5f;
        Vector3 hopForceVector = direction * hopForce; 
        rigidbody.AddForce(hopForceVector, ForceMode.Impulse); 
    }

    IEnumerator ReproductionTimer()
    {
        yield return new WaitForSeconds(reproductionCoolDown);
        this.canReproduce = true;
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        OnDeath();
    }

    IEnumerator TickTimer()
    {
        yield return new WaitForSeconds(Helpers.tickRate);
        this.hungerTime -= 0.5f;
        this.thristTime -= 0.5f;
        this.reproductionTime -= 0.5f;
        if (hungerTime <= 0 || thristTime <= 0)
        {
            OnDeath();
        }
        StartCoroutine(TickTimer());
    }
}