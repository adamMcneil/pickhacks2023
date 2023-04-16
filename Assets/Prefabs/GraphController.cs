using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    private Canvas canvas;
    private const float DATA_UPDATE_SECONDS = 0.5f;


    private float xStart = -960f;
    private float xEnd = 0;
    private float xRange = 0;
    private float xChange;
    private float yStart = -536f;
    private float yEnd= -176f;
    private float yRange = 0;

    private int numberOfPoints = 25;

    [SerializeField] private Image plantPoint;
    [SerializeField] private Image rabbitPoint;
    [SerializeField] private Image foxPoint;

    private List<RectTransform> plantPoints = new List<RectTransform>();
    private List<RectTransform> rabbitPoints = new List<RectTransform>();
    private List<RectTransform> foxPoints = new List<RectTransform>();

    private List<int> plantPopulation = new List<int>();
    private List<int> rabbitPopulation = new List<int>();
    private List<int> foxPopulation = new List<int>();

    private void Start()
    {
        canvas= GetComponent<Canvas>();
        
        xRange = xEnd - xStart;
        xChange = xRange / numberOfPoints;
        
        yRange = yEnd - yStart;

        for (int i = 0; i < numberOfPoints; i++)
        {
            Image instant = Instantiate(plantPoint, Vector3.zero, Quaternion.identity);
            instant.rectTransform.SetParent(transform);
            instant.rectTransform.localPosition = new Vector3(xStart + (i * xChange), yStart, 0);
            plantPoints.Add(instant.rectTransform);

            instant = Instantiate(rabbitPoint, Vector3.zero, Quaternion.identity);
            instant.rectTransform.SetParent(transform);
            instant.rectTransform.localPosition = new Vector3(xStart + (i * xChange), yStart, 0);
            rabbitPoints.Add(instant.rectTransform);

            instant = Instantiate(foxPoint, Vector3.zero, Quaternion.identity);
            instant.rectTransform.SetParent(transform);
            instant.rectTransform.localPosition = new Vector3(xStart + (i * xChange), yStart, 0);
            foxPoints.Add(instant.rectTransform);

            plantPopulation.Add(0);
            rabbitPopulation.Add(0);
            foxPopulation.Add(0);
        }
        StartCoroutine(UpdateData());
    }

    IEnumerator UpdateData()
    {
        yield return new WaitForSeconds(DATA_UPDATE_SECONDS);

        plantPopulation.Add(Helpers.bushes.Count);
        plantPopulation.RemoveAt(0);

        rabbitPopulation.Add(Helpers.GetRabbits().Count);
        rabbitPopulation.RemoveAt(0);

        foxPopulation.Add(Helpers.GetFoxes().Count);
        foxPopulation.RemoveAt(0);

        UpdateGraph();
        StartCoroutine(UpdateData());
    }

    private void UpdateGraph()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            plantPoints[i].localPosition = new Vector3(xStart + (i * xChange), yStart + plantPopulation[i] , 0);
            rabbitPoints[i].localPosition = new Vector3(xStart + (i * xChange), yStart + rabbitPopulation[i] , 0);
            foxPoints[i].localPosition = new Vector3(xStart + (i * xChange), yStart + foxPopulation[i] , 0);
        }
    }
}
