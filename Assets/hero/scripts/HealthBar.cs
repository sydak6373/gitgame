using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Text text;
    private HealthInteraction health;
    private float fill;

    public void Awake()
    {
        health = GetComponent<HealthInteraction>();
        fill = 1f;
    }

    public void Update()
    {
        fill = health.hitpoints / (float)health.maxHeatpoints;
        bar.fillAmount = fill;
        text.text = health.hitpoints.ToString(); ;
    }
}
