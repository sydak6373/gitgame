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
    private int i;
    public delegate void NPCAktions();
    public event NPCAktions OnNPCAktions;

    void Start()
    {
        i = 0;
        dialog = GetComponentInChildren<DialogScript>();
    }

    
    void Update()
    {
        
    }

    public void NPCInteraction()
    {
        
        if (i < dialogText.Length)
        {

            dialog.DialogInteractions(dialogText[i]);
            i++;
        }
        else
        {
            if (Vector3.Distance(transform.position, hero.transform.position) <= 1.5f)
            {
                OnNPCAktions?.Invoke();
                heroHealth.hitpoints = heroHealth.maxHeatpoints;
                hero.SavePlayer();
                dialog.DialogInteractions("Я запомню твои деяния. (Игра сохранена)");
            }
            else
            {
                
                dialog.DialogInteractions();
            }
            i = 0;
        }

    }
}
