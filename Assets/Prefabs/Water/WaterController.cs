using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    void Start()
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        foreach (var box in boxColliders)
        {
            Helpers.AddWater(box);
        }
    }
}
