using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;

    public int scene;
    public Text loadingText;
    public Canvas loadingCanvas;

    [ReadOnlyField]
    public float timer = 0;

    public List<Sprite> winterSprites;
    public List<Sprite> springSprites;
    public List<Sprite> summerSprites;
    public List<Sprite> fallSprites;

    public Image animatedImage;
    public Image nonAnimatedImage;

    public SeasonCycle season;

    void Update()
    {
        if (!loadScene)
        {
            if (loadingCanvas.enabled)
            {
                loadingCanvas.enabled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && !loadScene)
        {
            if (!loadingCanvas.enabled)
            {

                int rand = 0;
                switch (season.season)
                {
                    case "Winter":
                        rand = Random.Range(0, winterSprites.Count);
                        break;
                    case "Spring":
                        rand = Random.Range(0, springSprites.Count);
                        break;
                    case "Summer":
                        rand = Random.Range(0, summerSprites.Count);
                        break;
                    case "Fall":
                        rand = Random.Range(0, fallSprites.Count);
                        break;
                    default:
                        rand = Random.Range(0, springSprites.Count);
                        break;
                }
                
                if (rand == 0)
                {
                    animatedImage.enabled = true;
                }
                else
                {
                    nonAnimatedImage.sprite = winterSprites[rand];
                    nonAnimatedImage.enabled = true;
                }
                loadingCanvas.enabled = true;
            }
            loadScene = true;

            StartCoroutine(LoadNewScene());
        }

        if (loadScene)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
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
