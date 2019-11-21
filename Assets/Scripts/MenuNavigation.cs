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
            if (canvas != null)
            {
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
