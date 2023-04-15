using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float hopForce;
    [SerializeField] private float sightDistance;
    private Rigidbody rigidbody;
    private float lifeTime = 500;
    private float maxHunger = 60;
    private float maxThrist = 60;
    private float reproduceBufferTime = 20;
    public float reproduceTimer;
    public float hungerTime;
    public float thristTime;
    public float eatPause;
    private float eatPauseMax = 5f;
    public bool canReproduce = true;
    public GameObject foxObject;

    void Start()
    {
        hungerTime = maxHunger * .25f;
        thristTime = maxThrist * .5f;
        reproduceTimer = reproduceBufferTime;
        rigidbody = GetComponent<Rigidbody>();
        Helpers.AddFoxes(this.transform);
        StartCoroutine(LifeTimer());
        StartCoroutine(TickTimer());
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero || (rigidbody.velocity.magnitude > 0 && rigidbody.velocity.magnitude < 0.001f))
        {
            if (eatPause == 0)
            {
              CalculateRotation();
              Hop();
            }
        }
    }

    private void CalculateRotation()
    {
        if (rigidbody.velocity == Vector3.zero || (rigidbody.velocity.magnitude > 0 && rigidbody.velocity.magnitude < 0.001f))
        {
            Transform closestRabbit = Helpers.GetClosestRabbit(this.transform, sightDistance);
            Transform closestFox = Helpers.GetClosestFox(this.transform, sightDistance);
            Vector3 closestWater = Helpers.GetClosestWater(this.transform, sightDistance);
            if ((hungerTime > (maxHunger * .33f)) && (thristTime > (maxThrist * .33f)) && reproduceTimer <= 0 && closestFox != null)
            {
               Vector3 direction = closestFox.transform.position - this.transform.position;
               Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
               this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
               return; 
            }
            else if (closestRabbit != null && hungerTime <= thristTime)
            {
               Vector3 direction = closestRabbit.transform.position - this.transform.position;
               Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
               this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
               return;
            }
            else
            {
                if (closestWater != Vector3.zero && thristTime < (maxThrist * .25f))
                {
                    Vector3 direction = closestWater - this.transform.position;
                    Vector3 normilizedDirection = new Vector3(direction.x, 0, direction.z).normalized;
                    this.transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, normilizedDirection, Vector3.up) + 90, 0);
                    return;
                }
            }
            this.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
    }

    void Hop()
    {
        Vector3 direction = this.transform.forward + Vector3.up * 0.5f;
        Vector3 hopForceVector = direction * hopForce;
        rigidbody.AddForce(hopForceVector, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rabbit"))
        {
            hungerTime = maxHunger;
            eatPause = eatPauseMax;
        }
        if (collision.transform.CompareTag("Fox"))
        {
          if (reproduceTimer <= 0)
          {
            GameObject spawnedobject = Instantiate(foxObject, new Vector3(this.transform.position.x + 2f , this.transform.position.y, this.transform.position.z + 2f), this.transform.rotation);
            reproduceTimer = reproduceBufferTime;
          }
        }
        if (collision.transform.CompareTag("Water"))
        {
            thristTime = maxThrist;
        }
    }
    IEnumerator LifeTimer()
      {
          yield return new WaitForSeconds(lifeTime);
          OnDeath();
      }

      IEnumerator TickTimer()
      {
          yield return new WaitForSeconds(Helpers.tickRate);
          if (reproduceTimer > 0)
          { 
            reproduceTimer -= 0.5f;
          }
          if (eatPause != 0)
          {
            eatPause -= 0.5f;
          } else
          {
            this.hungerTime -= 0.5f;
            this.thristTime -= 0.5f;
            if (hungerTime <= 0 || thristTime <= 0)
            {
                OnDeath();
            }
          }
          StartCoroutine(TickTimer());
      }

    public void OnDeath()
    {
        Helpers.RemoveFoxes(this.transform);
        Destroy(this.gameObject);
    }
}