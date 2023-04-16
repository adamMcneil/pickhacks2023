using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float secondsInFullDay = 10f; // The number of seconds in a full day cycle.
    [Range(0, 1)]
    public float currentTimeOfDay = 0; // The current time of day represented as a value from 0 to 1.
    public float timeMultiplier = 1f; // A multiplier for the speed of the cycle.

    private float sunInitialIntensity;
    private float timeOfDayInSeconds;
    public Light sun;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSunRotation();
        UpdateTimeOfDay();
    }

    void UpdateSunRotation()
    {
        float anglePerSecond = 360f / secondsInFullDay;
        transform.RotateAround(Vector3.zero, Vector3.up, anglePerSecond * timeMultiplier * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }

    void UpdateTimeOfDay()
    {
        timeOfDayInSeconds += Time.deltaTime * timeMultiplier;
        currentTimeOfDay = timeOfDayInSeconds / secondsInFullDay;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            timeOfDayInSeconds = 0;
        }

        UpdateSunIntensity();
    }

    void UpdateSunIntensity()
    {
        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = .25f;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
