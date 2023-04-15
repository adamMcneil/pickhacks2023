using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static List<Transform> rabbits = new List<Transform>();
    private static List<Transform> foxes = new List<Transform>();
    public static List<Transform> bushes = new List<Transform>();
    public static List<BoxCollider> waters = new List<BoxCollider>();

    public static float tickRate = 0.5f; // in seconds 

    #region Rabbits
    public static List<Transform> GetRabbits() { return rabbits; }

    public static void AddRabbit(Transform newRabbit)
    {
        rabbits.Add(newRabbit);
    }

    public static void RemoveRabbit(Transform oldRabbit)
    {
        rabbits.Remove(oldRabbit);
    }

    public static Transform GetClosestRabbit(Transform myTransform, float sightDistance)
    {
        Transform closestRabbit= null;
        float distance = sightDistance * sightDistance;

        foreach (var rabbit in rabbits)
        {
            float temp = (rabbit.position - myTransform.position).sqrMagnitude;
            if (temp < distance && myTransform != rabbit)
            {
                closestRabbit = rabbit;
                distance = temp;
            }
        }
        return closestRabbit;
    }
    #endregion

    #region Fox

    public static List<Transform> GetFoxes() { return foxes; }

    public static void AddFoxes(Transform newFox)
    {
        foxes.Add(newFox);
    }

    public static void RemoveFoxes(Transform oldFox)
    {
        foxes.Remove(oldFox);
    }

    public static Transform GetClosestFox(Transform myTransform, float sightDistance)
    {
        Transform closestFox = null;
        float distance = sightDistance * sightDistance;

        foreach (var fox in foxes)
        {
            float temp = (fox.position - myTransform.position).sqrMagnitude;
            if (temp < distance && myTransform != fox)
            {
                closestFox = fox;
                distance = temp;
            }
        }
        return closestFox;
    }
  #endregion

    #region Bush
    public static void AddBushes(Transform newBush)
    {
        bushes.Add(newBush);
    }

    public static void RemoveBushes(Transform oldBush)
    {
        bushes.Remove(oldBush);
    }

    public static Transform GetClosestBush(Transform myTransform, float sightDistance)
    {
        Transform closestBush = null;
        float distance = sightDistance * sightDistance;

        foreach (var bush in bushes)
        {
            float temp = (bush.position - myTransform.position).sqrMagnitude;
            if (temp < distance && myTransform != bush)
            {
                closestBush = bush;
                distance = temp;
            }
        }
        return closestBush;
    }
    #endregion

    #region Water

    public static void AddWater(BoxCollider newWater)
    {
        waters.Add(newWater);
    }

    public static void RemoveWater(BoxCollider oldWater)
    {
        waters.Remove(oldWater);
    }

    public static Vector3 GetClosestWater(Transform myTransform, float sightDistance)
    {
        Vector3 closestWaterPoint = Vector3.zero;
        float distance = sightDistance * sightDistance;

        foreach (var water in waters)
        {
            Vector3 currentWaterPoint = water.ClosestPoint(myTransform.position);
            float temp = (currentWaterPoint - myTransform.position).sqrMagnitude;
            if (temp < distance)
            {
                closestWaterPoint = currentWaterPoint;
                distance = temp;
            }
        }
        return closestWaterPoint;
    }

    #endregion
}
