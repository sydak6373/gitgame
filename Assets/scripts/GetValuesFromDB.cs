using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System;
using System.Globalization;

class GetValuesFromDB
{
    public float DbGetCost(float lvl)
    {
        float cost = float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT {"UpgradeCost"} FROM {"HeroLvl"} WHERE {"Lvl"} = {lvl + 1}"));
        return cost;
    }

    private void DbSetStats(string skillName, int lvl, float HeroLvl, float value)
    {
        DbHelper.ExecuteWithoutAnswer($"UPDATE {"HeroCurrentStats"} SET {"skillLvl"} = {lvl}, {"statvalue"} = {value.ToString(CultureInfo.InvariantCulture)} WHERE {"skillName"} = '{skillName}'");
        DbHelper.ExecuteWithoutAnswer($"UPDATE {"HeroCurrentStats"} SET {"skillLvl"} = {HeroLvl.ToString(CultureInfo.InvariantCulture)}, {"statvalue"} = {HeroLvl.ToString(CultureInfo.InvariantCulture)} WHERE {"skillName"} = '{"Lvl"}'");
    }

    public void DbSetStats(string skillName, float value)
    {
        if (skillName == "Lvl")
        {
            DbHelper.ExecuteWithoutAnswer($"UPDATE {"HeroCurrentStats"} SET {"skillLvl"} = {value.ToString(CultureInfo.InvariantCulture)}, {"statvalue"} = {value.ToString(CultureInfo.InvariantCulture)} WHERE {"skillName"} = '{skillName}'");
        }
        else
        {
            int lvl = int.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT {"skillLvl"} FROM {"HeroStats"} WHERE {"skillName"} = '{skillName}' AND {"statvalue"} = {value.ToString(CultureInfo.InvariantCulture)}"));
            DbHelper.ExecuteWithoutAnswer($"UPDATE {"HeroCurrentStats"} SET {"skillLvl"} = {lvl}, {"statvalue"} = {value.ToString(CultureInfo.InvariantCulture)} WHERE {"skillName"} = '{skillName}'");
        }
    }

    public float DbGetNextStatValue(string skillName, float statValue, float HeroLvl)
    {
        int lvl = int.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT {"skillLvl"} FROM {"HeroStats"} WHERE {"skillName"} = '{skillName}' AND {"statvalue"} = {statValue.ToString(CultureInfo.InvariantCulture)}"));
        float nextval = float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT {"statvalue"} FROM {"HeroStats"} WHERE {"skillName"} = '{skillName}' AND {"skillLvl"} = {lvl + 1}"));
        DbSetStats(skillName, lvl + 1, HeroLvl + 1, nextval);
        return nextval;
    }

    public float DbGetCurrentStatValue(string skillName)
    {
        return float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT {"statvalue"} FROM {"HeroCurrentStats"} WHERE {"skillName"} = '{skillName}'"));
    }

    public Dictionary<string, float> DbGetHeroStats()
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        List<string> skillNames = new List<string>() { "Strength", "MaxHp", "MaxStamina", "MaxMana", "CritChance", "CritDamage", "Speed", "Intellegence", "Haste", "Lvl" };
        for (int i = 0; i < skillNames.Count; i++)
        {
            dict.Add($"{skillNames[i]}", float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT {"statvalue"} FROM {"HeroCurrentStats"} WHERE {"skillName"} = '{skillNames[i]}'")));
        }
        return dict;
    }
}