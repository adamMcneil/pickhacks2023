using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void WriteFile(string fileName, string text) {
        string path = Application.persistentDataPath + "/" + fileName;
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
    }

    public void End() {
        System.DateTime dt = System.DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd_HH-mm-ss");
        //Debug.Log(Application.persistentDataPath);
        WriteFile(time + "_eco_sim_data.csv", dataManager.ToCSV());
    }
}
