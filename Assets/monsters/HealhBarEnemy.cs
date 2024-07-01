using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealhBarEnemy : MonoBehaviour
{
    [SerializeField] private GameObject HPbar;
    [SerializeField] private Vector3 offset;
    private HealthInteraction health;
    private float fill;
    private float maxHP; 
    private GameObject bar;
    private string enemyName;
    
    public void Awake()
    {
        enemyName = transform.parent.tag;
        health = GetComponentInParent<HealthInteraction>();
        bar = HPbar.transform.Find("Filler").gameObject;
        fill = 1f;
        switch (enemyName)
        {
            case "zombe": maxHP = 50f; break;
            case "skelet": maxHP = 30f; break;
            case "Something": maxHP = 300f; break;
            default: break;
        }

    }

    public void Update()
    {
        if((fill < 1f) && (fill > 0f)) { HPbar.SetActive(true); }
        else { HPbar.SetActive(false); }
        HPbar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        fill = health.hitpoints / maxHP;

        
        bar.GetComponent<Image>().fillAmount = fill;
    }

}
