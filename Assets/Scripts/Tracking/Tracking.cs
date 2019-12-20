using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Tracking : MonoBehaviour
{

    public float totalGameTime = 0.0f;
    private string currentScene = "";
    private int currentSceneIndex = 0;
    public class FloatEvent
    {
        public string eventName;
        public float total;

        public FloatEvent(string name)
        {
            eventName = name;
            total = 0.0f;
        }
        public FloatEvent(string name, float startingpoint)
        {
            eventName = name;
            total = startingpoint;
        }
    }
    public class IntEvent
    {
        public string eventName;
        public int eventTriggers;

        public IntEvent(string name)
        {
            eventName = name;
            eventTriggers = 0;
        }
        public IntEvent(string name, int startingpoint)
        {
            eventName = name;
            eventTriggers = startingpoint;
        }
    }
    private List<FloatEvent> sceneTimes = new List<FloatEvent>();
    private List<FloatEvent> FloatEvents = new List<FloatEvent>();
    private List<IntEvent> intEvents = new List<IntEvent>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentScene = SceneManager.GetActiveScene().name;
        sceneTimes.Add(new FloatEvent(currentScene));

        
       // Debug.Log(DateTime.Now.ToString("hhmmss"));
        
        //Debug.Log(DateTime.Today.Minute.ToString());
    


    }
    void Update()
    {
        totalGameTime += Time.deltaTime;
        SceneTimeTracker();
        //for (int i = 0; i < sceneTimes.Count; i++)
        //{
        //    Debug.Log(sceneTimes[i].sceneName + " " + sceneTimes[i].totalTime);
        //}

    }
    private void OnApplicationQuit()
    {
        //add functions that save the various lists to files
        SaveTime();
        SaveFloatEvents(sceneTimes, "Scenetimes");
        if(FloatEvents.Count != 0)
        {
            SaveFloatEvents(FloatEvents, "FloatEvents");
        }
        if (intEvents.Count != 0)
        {
            SaveIntEvents(intEvents, "IntEvents");
        }
    }

    private void SceneTimeTracker()
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            string newScene = SceneManager.GetActiveScene().name;
            bool found = false;
            for (int i = 0; i < sceneTimes.Count; i++)
            {
                if (sceneTimes[i].eventName == newScene)
                {
                    currentScene = sceneTimes[i].eventName;
                    currentSceneIndex = i;
                    found = true;
                }
            }
            if (!found)
            {
                sceneTimes.Add(new FloatEvent(newScene));
                currentScene = newScene;
                currentSceneIndex = sceneTimes.Count - 1;
            }

        }
        //currentScene = SceneManager.GetActiveScene().name;
        sceneTimes[currentSceneIndex].total += Time.deltaTime;
    }

    public void TrackIntEvent(string eventName, int iteration)
    {
        bool found = false;
        for (int i = 0; i < intEvents.Count; i++)
        {
            if (intEvents[i].eventName == eventName)
            {
                intEvents[i].eventTriggers += iteration;
                found = true;
                break;
            }
        }
        if (!found)
        {
            intEvents.Add(new IntEvent(eventName, iteration));
        }
    }
    public void TrackFloatEvent(string eventName, float iteration)
    {
        bool found = false;
        for (int i = 0; i < intEvents.Count; i++)
        {
            if (FloatEvents[i].eventName == eventName)
            {
                FloatEvents[i].total += iteration;
                found = true;
                break;
            }
        }
        if (!found)
        {
            FloatEvents.Add(new FloatEvent(eventName, iteration));
        }
    }

    private void SaveFloatEvents(List<FloatEvent> events, string fileName)
    {
        string[] stringyfloats = new string[events.Count];

        for (int i = 0; i < events.Count; i++)
        {
            stringyfloats[i] =
                events[i].eventName + "," + events[i].total;
        }

        string destination = Application.persistentDataPath + "/" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, stringyfloats);
        file.Close();
    }
    private void SaveIntEvents(List<IntEvent> events, string fileName)
    {
        string[] stringyfloats = new string[events.Count];

        for (int i = 0; i < events.Count; i++)
        {
            stringyfloats[i] =
                events[i].eventName + "," + events[i].eventTriggers;
        }

        string destination = Application.persistentDataPath + "/" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, stringyfloats);
        file.Close();
    }
    private void SaveTime()
    {
        string path = Application.persistentDataPath + "/TotalTime.txt";
        if (File.Exists(path))
        {
            File.AppendAllText(path, "," + totalGameTime.ToString());
        }
        else
        {
            File.AppendAllText(path, totalGameTime.ToString());
        }

        

        

    }
    
}
