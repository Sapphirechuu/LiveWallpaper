using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;

    public int scene;
    public Text loadingText;
    public Canvas loadingCanvas;

    [ReadOnlyField]
    public float timer = 0;

    public List<Sprite> morningSprites;
    public List<Sprite> daySprites;
    public List<Sprite> eveningSprites;
    public List<Sprite> nightSprites;

    private string spriteListToUse;

    public Image animatedImage;
    public Image nonAnimatedImage;

    private DaylightCycle daylight;

    private void Start()
    {
        daylight = gameObject.GetComponent<DaylightCycle>();
    }

    void Update()
    {
        if (!loadScene)
        {
            if (loadingCanvas.enabled)
            {
                loadingCanvas.enabled = false;
            }
        }

        if (loadScene)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    public void LoadScene(int sceneNum)
    {
        if (sceneNum <= /*EditorBuildSettings.scenes.Length - 1*/2)
        {            
            scene = sceneNum;
        }
        else
        {
            Debug.Log("The selected scene is not in the build settings, loading default scene. Button: " + EventSystem.current.currentSelectedGameObject.name);

            scene = 0;
        }
        if (!loadingCanvas.enabled)
        {

            int rand = 0;
            if (daylight.morning)
            {
                rand = Random.Range(0, morningSprites.Count);
                spriteListToUse = "Morning";
            }
            else if (daylight.day)
            {
                rand = Random.Range(0, daySprites.Count);
                spriteListToUse = "Day";
            }
            else if (daylight.evening)
            {
                rand = Random.Range(0, eveningSprites.Count);
                spriteListToUse = "Evening";
            }
            else
            {
                rand = Random.Range(0, nightSprites.Count);
                spriteListToUse = "Night";
            }

            if (rand == 0)
            {
                animatedImage.enabled = true;
            }
            else
            {
                switch (spriteListToUse)
                {
                    case "Morning":
                        nonAnimatedImage.sprite = morningSprites[rand];
                        break;
                    case "Day":
                        nonAnimatedImage.sprite = daySprites[rand];
                        break;
                    case "Evening":
                        nonAnimatedImage.sprite = eveningSprites[rand];
                        break;
                    case "Night":
                        nonAnimatedImage.sprite = nightSprites[rand];
                        break;
                    default:
                        nonAnimatedImage.sprite = nightSprites[rand];
                        break;
                }
                nonAnimatedImage.sprite = morningSprites[rand];
                nonAnimatedImage.enabled = true;
            }
            loadingCanvas.enabled = true;
        }
        loadScene = true;
        if (scene == -1)
        {
            Application.Quit();
        }
        else
        {
            StartCoroutine(LoadNewScene());
        }
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
                loadScene = false;
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
