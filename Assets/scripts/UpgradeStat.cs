using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class UpgradeStat
{
    public void Upgrade(string skillName, float statValue, ExpHolder eh, GetValuesFromDB db, float lvl)
    {
        float cost = float.MaxValue;
        try
        {
            cost = db.DbGetCost(lvl);
        }
        catch { }
        if (eh.exp >= cost)
        {
            try
            {
                statValue = db.DbGetNextStatValue(skillName, statValue, lvl);
                eh.DecreaseExp(cost);
                //Console.WriteLine("upgrade sucssessfull");
            }
            catch
            {
                //Debug.Log("MaxLvlReached");
            }
        }
    }
}
