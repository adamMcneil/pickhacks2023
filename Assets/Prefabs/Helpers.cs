using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static List<GameObject> rabbits = new List<GameObject>();

    public static void AddRabbit(GameObject newRabbit)
    {
        rabbits.Add(newRabbit);
    }
    
}
