using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static List<Transform> rabbits = new List<Transform>();
    private static List<Transform> foxes = new List<Transform>();

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
}
