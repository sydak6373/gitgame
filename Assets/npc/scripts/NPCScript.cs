using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private string[] dialogText;
    [SerializeField] public HealthInteraction heroHealth;
    [SerializeField] private Hero hero;
    private DialogScript dialog;
    private const float NPC_DIALOG_PANEL_TIME_LIFE = 5f;
    private int i;
    private float timer;
    public delegate void NPCAktions();
    public event NPCAktions OnNPCAktions;

    void Start()
    {
        timer = 0;
        i = 0;
        dialog = GetComponentInChildren<DialogScript>();
    }

    
    void FixedUpdate()
    {
        if(i != 0) timer += Time.deltaTime;
        if (timer > NPC_DIALOG_PANEL_TIME_LIFE)
        { 
            dialog.DialogInteractions();
            i = 0;
            timer = 0;
        }
    }

    public void NPCInteraction()
    {
        
        if (i < dialogText.Length)
        {
            timer = 0;
            dialog.DialogInteractions(dialogText[i]);
            i++;
        }
        else
        {
            if (Vector3.Distance(transform.position, hero.transform.position) <= 1.5f)
            {
                
                heroHealth.hitpoints = heroHealth.maxHeatpoints;
                OnNPCAktions?.Invoke();
                //hero.SavePlayer();
                // dialog.DialogInteractions("Я запомню твои деяния. (Игра сохранена)");
            }  
            dialog.DialogInteractions();
            i = 0;
        }

    }
}
