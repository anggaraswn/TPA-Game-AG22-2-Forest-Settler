using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup canvas;

    public void SetName(string name)
    {
        text.text = name;
    }

    public void ShowUI()
    {
        canvas.alpha = 1;
    }

    public void HideUI()
    {
        canvas.alpha = 0;
    }
}
