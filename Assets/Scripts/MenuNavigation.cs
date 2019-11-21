using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MenuNavigation : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas habitatCanvas;

    public void ChangeView(string canvasToLoad)
    {
        if (canvasToLoad == "habitat")
        {
            habitatCanvas.enabled = true;
            mainCanvas.enabled = false;
        }
        else if (canvasToLoad == "main")
        {
            mainCanvas.enabled = true;
            habitatCanvas.enabled = false;
        }
        else
        {
            Debug.Log("Canvas does not exist. Button: " + EventSystem.current.currentSelectedGameObject.name);
        }
    }
}
