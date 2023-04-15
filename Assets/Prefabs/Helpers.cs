using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static List<Transform> rabbits = new List<Transform>();

    public static List<Transform> GetRabbits() { return rabbits; }

    public static void AddRabbit(Transform newRabbit)
    {
        rabbits.Add(newRabbit);
    }

    public static void RemoveRabbit(Transform oldRabbit)
    {
        rabbits.Remove(oldRabbit);
    }
    
}
