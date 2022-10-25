using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public CanvasGroup canvas;
    public TextMeshProUGUI enemyName;
    public Enemy enemy;

    private void Start()
    {
        canvas = GetComponentInParent<CanvasGroup>();
        slider = GetComponent<Slider>();
        //enemyName = GetComponentInParent<TextMeshProUGUI>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void Show()
    {
        canvas.alpha = 1;
    }

    public void Hide()
    {
        canvas.alpha = 0;
    }

    public void SetName(string name)
    {
        enemyName.text = name;
    }

}
