using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2.0f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 3.0f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 4.0f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale = 5.0f;
        }
    }
}
