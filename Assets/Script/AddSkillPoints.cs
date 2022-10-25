using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddSkillPoints : MonoBehaviour
{
    public CanvasGroup canvas;
    public TextMeshProUGUI statusPoint;
    public TextMeshProUGUI currAGI;
    public TextMeshProUGUI currSTR;
    public TextMeshProUGUI currPow;

    private void Start()
    {
        statusPoint = GameObject.Find("CurrPoint").GetComponent<TextMeshProUGUI>();
        currAGI = GameObject.Find("CurrAgi").GetComponent<TextMeshProUGUI>();
        currSTR = GameObject.Find("CurrSTR").GetComponent<TextMeshProUGUI>();
        currPow = GameObject.Find("CurrPOW").GetComponent<TextMeshProUGUI>();
    }

    public void Show()
    {
        canvas.alpha = 1; 
    }

    public void Hide()
    {
        canvas.alpha = 0;
    }

    public void SetAGI(int set)
    {
        currAGI.text = set.ToString();
    }

    public void SetSTR(int set)
    {
        currSTR.text = set.ToString();
    }

    public void SetPOW(int set)
    {
        currPow.text = set.ToString();
    }

    public void SetStatusPoint(int set)
    {
        statusPoint.text = set.ToString();
    }
}
