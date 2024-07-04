using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInteraction : MonoBehaviour
{
    public int maxHeatpoints;
    [NonSerialized] public int hitpoints;
    public Action<int, int> OnChange;
    private float lelevHP;

    public void Start()
    {
        lelevHP = 0;
        hitpoints = maxHeatpoints;
    }
    public void Change(int amount)
    {   if (((hitpoints + amount) >= 0) && ((hitpoints + amount) <= maxHeatpoints))
        {
            hitpoints += amount;
            OnChange?.Invoke(hitpoints, amount);
            Debug.Log(hitpoints);
        }
        else if ((hitpoints + amount) > maxHeatpoints) 
        {
            hitpoints = maxHeatpoints;
            OnChange?.Invoke(hitpoints, amount);
            Debug.Log(hitpoints);

        }
        else
        {
            hitpoints = 0;
            OnChange?.Invoke(hitpoints, amount);
            Debug.Log(hitpoints);
        }
    }

    public void MaxHpUp()
    {
        if (lelevHP <= 50)
        {
            maxHeatpoints += ((int)(maxHeatpoints * (0.15 - (lelevHP * 0.003f))));
            hitpoints = maxHeatpoints;
            ++lelevHP;
        }
    }


}
