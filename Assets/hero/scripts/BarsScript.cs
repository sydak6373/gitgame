using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsScript : MonoBehaviour
{
    [SerializeField] private Image barHp;
    [SerializeField] private Image barSt;
    [SerializeField] private Image barMp;

    private HealthInteraction health;
    private ManaInteraction mana;
    private StaminaInteraction stamina;

    private float fillHp;
    private float fillMp;
    private float fillSt;

    public void Awake()
    {
        health = GetComponent<HealthInteraction>();
        mana = GetComponent<ManaInteraction>();
        stamina = GetComponent<StaminaInteraction>();

        fillHp = 1f;
        fillMp = 1f;
        fillSt = 1f;
    }

    public void Update()
    {
        fillMp = mana.PersCurrentMana / (float)mana.PersMaxMana;
        barMp.fillAmount = fillMp;

        fillSt = stamina.PersCurrentStamina / (float)stamina.PersMaxStamina;
        barSt.fillAmount = fillSt;

        fillHp = health.hitpoints / (float)health.maxHeatpoints;
        barHp.fillAmount = fillHp;
       
    }
}
