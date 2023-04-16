using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const float DATA_UPDATE_SECONDS = 0.5f;

    private static List<int> numBushes = new List<int>();
    private static List<int> numRabbits = new List<int>();
    private static List<int> numFoxes = new List<int>();

    // Start is called before the first frame update

    public void OnStart()
    {
        numBushes = new List<int>();
        numRabbits = new List<int>();
        numFoxes = new List<int>();
        StartCoroutine(UpdateData());
    }

    public void OnPause()
    {
        StopAllCoroutines();
    }

    IEnumerator UpdateData() {
        yield return new WaitForSeconds(DATA_UPDATE_SECONDS);
        numBushes.Add(Helpers.bushes.Count);
        numRabbits.Add(Helpers.GetRabbits().Count);
        numFoxes.Add(Helpers.GetFoxes().Count);
        StartCoroutine(UpdateData());
    }

    public string ToCSV() {
        string csv = "Time,Bushes,Rabbits,Foxes\n";
        int n = numRabbits.Count;
        for (int i = 0; i < n; i++) {
            csv += DATA_UPDATE_SECONDS * i + "," + numBushes[i] + "," + numRabbits[i] + "," + numFoxes[i] + "\n";
        }
        return csv;
    }
}
