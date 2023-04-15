using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private DataManager dataManager;
    [SerializeField] private Button pauseButton;
    private bool paused = false;

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

    public void TogglePaused() {
        if (paused) {
            paused = false;
            pauseButton.GetComponentInChildren<Text>().text = "Pause and Save";
            Resume();
        } else {
            paused = true;
            pauseButton.GetComponentInChildren<Text>().text = "Resume";
            Pause();
        }
    }

    public void Pause() {
        System.DateTime dt = System.DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd_HH-mm-ss");
        //Debug.Log(Application.persistentDataPath);
        WriteFile(time + "_eco_sim_data.csv", dataManager.ToCSV());
        Time.timeScale = 0;
    }

    public void Resume() {
        Time.timeScale = 1;
    }
}
