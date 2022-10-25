using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;

    public void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
        slider.value = exp;
    }

    public void SetExp(int exp)
    {
        slider.value = exp;
    }

    public void SetCurrLevel(int level)
    {
        text.text = "Lvl " + level;
    }
}
