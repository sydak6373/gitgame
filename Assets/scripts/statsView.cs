using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class statsView : MonoBehaviour
{
    [SerializeField] public HealthInteraction healthInteraction;
    [SerializeField] public ManaInteraction manaInteraction;
    [SerializeField] public StaminaInteraction staminaInteraction;
    [SerializeField] public ExpHolder expHolder;

    private int expToTheNextLvl;
    void Start() 
    {
        

    }
    void Update()
    {
        expToTheNextLvl = (int)(Mathf.Log(Hero.Lvl, 2) * Hero.Lvl + Hero.Lvl);
        transform.GetChild(4).gameObject.GetComponent<TMP_Text>().text = expHolder.exp.ToString();
        transform.GetChild(6).gameObject.GetComponent<TMP_Text>().text = expToTheNextLvl.ToString();


        transform.GetChild(8).gameObject.GetComponent<TMP_Text>().text = healthInteraction.maxHeatpoints.ToString();
        transform.GetChild(10).gameObject.GetComponent<TMP_Text>().text = manaInteraction.PersMaxMana.ToString();
        transform.GetChild(12).gameObject.GetComponent<TMP_Text>().text = staminaInteraction.PersMaxStamina.ToString();

        transform.GetChild(14).gameObject.GetComponent<TMP_Text>().text = Movement.currentMovementSpeed.ToString();
        transform.GetChild(16).gameObject.GetComponent<TMP_Text>().text = Weapon.damageSkale.ToString();
        transform.GetChild(18).gameObject.GetComponent<TMP_Text>().text = Hero.Intelegent.ToString();

        transform.GetChild(20).gameObject.GetComponent<TMP_Text>().text = Hero.critShance.ToString();
        transform.GetChild(22).gameObject.GetComponent<TMP_Text>().text = Hero.critDAmage.ToString();
        transform.GetChild(24).gameObject.GetComponent<TMP_Text>().text = Hero.haste.ToString();
    }
}
