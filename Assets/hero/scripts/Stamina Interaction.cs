using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaInteraction : MonoBehaviour
{
    public int PersMaxStamina; 
    public int PersCurrentStamina;
    public Action<int, int> OnChange;
    private float timer;

    public void Start()
    {
        timer = 0f;

        PersCurrentStamina = PersMaxStamina;

    }

    private void FixedUpdate()
    {
        if (PersCurrentStamina < PersMaxStamina)
        {
            if (timer >= 0.5f)
            {
                timer = 0;
                Change((int)(PersMaxStamina * 0.05f));
            }
            timer += Time.deltaTime;
        }
    }

    public void Change(int amount)
    {
        if (((PersCurrentStamina + amount) >= 0) && ((PersCurrentStamina + amount) <= PersMaxStamina))
        {
            PersCurrentStamina += amount;
            OnChange?.Invoke(PersCurrentStamina, amount);

        }
        else if ((PersCurrentStamina + amount) > PersMaxStamina)
        {
            PersCurrentStamina = PersMaxStamina;
            OnChange?.Invoke(PersCurrentStamina, amount);


        }
        else
        {
            PersCurrentStamina = 0;
            OnChange?.Invoke(PersCurrentStamina, amount);

        }
    }
}
