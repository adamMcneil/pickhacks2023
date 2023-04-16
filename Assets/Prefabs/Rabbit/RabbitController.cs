using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RabbitController : MonoBehaviour
{
    [SerializeField] private float hopForce; // The force applied to the hop
    [SerializeField] private float sightDistance;
    [SerializeField] private TextMeshProUGUI hunger;
    [SerializeField] private TextMeshProUGUI thrist;
    [SerializeField] private TextMeshProUGUI reproduction;

    public GameObject rabbitObject;

    private Rigidbody rigidbody; // Reference to the Rigidbody component

    private float lifeTime = 150;
    private float maxHunger = 100;
    private float maxThrist = 100;
    private float maxReproduction = 10f;
    public float hungerTime;
    public float thristTime;
    public float reproductionTime;
    public bool hasEaten = false;
    private bool canReproduce = true;
    private float reproductionCoolDown = 10f;
    private float maxBabyTime = 20;
    private float babyTime = 0;
    public bool gender = true;
   public float foodAndWaterVision = 100;
    public float rabbitViewDistance = 100;

    void Start()
    {
        hungerTime = maxHunger * .25f;
        thristTime = maxThrist *.25f;
        reproductionTime = maxReproduction;
        rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
        Helpers.AddRabbit(this.transform);
        StartCoroutine(LifeTimer());
        StartCoroutine(TickTimer());
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero || (rigidbody.velocity.magnitude > 0 && rigidbody.velocity.magnitude < 0.001f))
        {
            CalculateRotation();
            Hop();
        }
    }

    private void CalculateRotation()
    {
        Transform closestFox = Helpers.GetClosestFox(this.transform, sightDistance);
        Transform closestRabbit = Helpers.GetClosestRabbit(this.transform, rabbitViewDistance);
        Transform closestBush = Helpers.GetClosestBush(this.transform, foodAndWaterVision);
        Vector3 closestWater = Helpers.GetClosestWater(this.transform, foodAndWaterVision);
        if (closestFox != null)
        {
            Vector3 direction = this.transform.position - closestFox.transform.position;
            Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
            this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            return;
        }
        else if (hungerTime < thristTime && (hungerTime <= maxHunger * .25f) || !hasEaten)
        {
          if (closestBush != null)
          {
            Vector3 direction = closestBush.transform.position - this.transform.position;
            Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
            this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            return;
          }
        }
        else if(reproductionTime <= 0)
        {
          if (closestRabbit != null && closestRabbit.gameObject.GetComponent<RabbitController>().babyTime <= 0 && closestRabbit.GetComponent<RabbitController>().reproductionTime <= 0)
          {
            Vector3 direction = closestRabbit.transform.position - this.transform.position;
            Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
            this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
            return;
          }
            else if (closestRabbit != null && closestRabbit.gameObject.GetComponent<RabbitController>().babyTime <= 0 && closestRabbit.GetComponent<RabbitController>().reproductionTime > 0)
            {
                Vector3 direction = this.transform.position - closestRabbit.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
                return;
            }
        }
        else
        {
            if (closestWater != Vector3.zero && (thristTime <= maxThrist * .25f))
            {
                Vector3 direction = closestWater - this.transform.position;
                Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
                return;
            }
        }
        this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fox"))
        {
            Helpers.RemoveRabbit(this.transform);
            Destroy(this.gameObject);
        } 
        if (collision.transform.CompareTag("Rabbit") && collision.gameObject.GetComponent<RabbitController>().babyTime <= 0 && reproductionTime <= 0 && babyTime <= 0 && hasEaten)
        {
            GameObject spawnedobject = Instantiate(rabbitObject, new Vector3(this.transform.position.x + 2f , this.transform.position.y, this.transform.position.z + 2f), this.transform.rotation);
            spawnedobject.GetComponent<RabbitController>().MakeRabbit(false);
            spawnedobject.GetComponent<RabbitController>().StartBaby();
            spawnedobject = Instantiate(rabbitObject, new Vector3(this.transform.position.x - 2f , this.transform.position.y, this.transform.position.z - 2f), this.transform.rotation);
            spawnedobject.GetComponent<RabbitController>().MakeRabbit(false);
            spawnedobject.GetComponent<RabbitController>().StartBaby();
            spawnedobject = Instantiate(rabbitObject, new Vector3(this.transform.position.x - 2f , this.transform.position.y, this.transform.position.z + 2f), this.transform.rotation);
            spawnedobject.GetComponent<RabbitController>().MakeRabbit(false);
            spawnedobject.GetComponent<RabbitController>().StartBaby();
            OnReproduce();
        }
        if (collision.transform.CompareTag("Bush"))
        {
            hungerTime = maxHunger;
            hasEaten = true;
        }
        if (collision.transform.CompareTag("Water"))
        {
            thristTime = maxThrist;
        }
    }

    public void OnDeath()
    {
        Helpers.RemoveRabbit(this.transform);
        Destroy(this.gameObject);
    }

    public void StartBaby() {
        this.babyTime = maxBabyTime;
        hasEaten = false;
        this.transform.localScale *= 0.5f;
    }

    public void MakeRabbit(bool canReproduce)
    {
        this.canReproduce = canReproduce;
        StartCoroutine(ReproductionTimer());
    }
    public void OnReproduce()
    {
        canReproduce = false;
        reproductionTime = maxReproduction;
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
        if (this.babyTime > 0) {
            this.babyTime -= 0.5f;
            if (this.babyTime <= 0) {
                this.transform.localScale *= 2;
            }
        }
        hunger.text = this.hungerTime.ToString();
        thrist.text = this.thristTime.ToString();
        reproduction.text = this.reproductionTime.ToString();
        if (hungerTime <= 0 || thristTime <= 0)
        {
            OnDeath();
        }
        StartCoroutine(TickTimer());
    }
}