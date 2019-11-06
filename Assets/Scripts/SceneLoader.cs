using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;
    public Canvas loadingCanvas;

    [ReadOnlyField]
    public float timer = 0;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !loadScene)
        {
            if (!loadingCanvas.enabled)
            {
                Debug.Log("Loading not enabled");
                loadingCanvas.enabled = true;
                Debug.Log(loadingCanvas.enabled);
            }

            //loadingText.text = "Loading...";
            loadScene = true;

            StartCoroutine(LoadNewScene());
        }

        //if (loadScene)
        //{
        //    loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        //}
    }

    IEnumerator LoadNewScene()
    {
        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            timer += Time.deltaTime;
            loadingText.text = "Loading progress: " + (async.progress * 100) + "%";

            if (async.progress >= 0.9f && timer >= 3.0f)
            {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
        ////yield return new WaitForSeconds(3);
        

        //AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        //yield return async.isDone;

        //while (!async.isDone)
        //{
        //    yield return null;
        //}
    }
}
