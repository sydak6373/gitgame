using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaInteraction : MonoBehaviour
{
    public int PersMaxMana; 
    public int PersCurrentMana;
    public Action<int, int> OnChange;
    private float timer;

    public void Start()
    {
        timer = 0f;
        PersCurrentMana = PersMaxMana;
         
    }

    private void FixedUpdate()
    {
        if(PersCurrentMana < PersMaxMana)
        {
            if(timer >= 0.5f)
            {
                timer = 0;
                Change((int)(PersMaxMana * 0.05f));
            }
            timer += Time.deltaTime;
        }
    }

    public void Change(int amount)
    {
        if (((PersCurrentMana + amount) >= 0) && ((PersCurrentMana + amount) <= PersMaxMana))
        {
            PersCurrentMana += amount;
            OnChange?.Invoke(PersCurrentMana, amount);
            
        }
        else if ((PersCurrentMana + amount) > PersMaxMana)
        {
            PersCurrentMana = PersMaxMana;
            OnChange?.Invoke(PersCurrentMana, amount);
           

        }
        else
        {
            PersCurrentMana = 0;
            OnChange?.Invoke(PersCurrentMana, amount);
            
        }
    }
}
