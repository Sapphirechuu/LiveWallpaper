using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MenuNavigation : MonoBehaviour
{
    public List<Canvas> canvasList;

    public void ChangeView(string canvasToLoad)
    {
        foreach (Canvas canvas in canvasList)
        {
            if (canvasToLoad == "Close")
            {
                canvas.enabled = false;
            }
            else
            {
                if (canvas != null)
                {
                    Debug.Log("Canvas isn't null");
                    if (!canvas.gameObject.name.Contains(canvasToLoad))
                    {
                        canvas.enabled = false;
                    }

                    else
                    {
                        canvas.enabled = true;
                    }
                }
                
            }
        }
    }
}
