using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System;

class StorableInfo
{
    //Вызывать это для генерации в бд
    static public void GenerateAllValuesForDB()
    {
       
        try
        {
            DbHelper.ExecuteWithoutAnswer("DROP TABLE HeroStats");
        }
        catch { }
        try
        {
            DbHelper.ExecuteWithoutAnswer("DROP TABLE HeroLvl");
        }
        catch { }
        DbHelper.ExecuteWithoutAnswer("CREATE TABLE HeroLvl (Lvl int, UpgradeCost float)");
        GenerateLvl(250, 2, 100);
        DbHelper.ExecuteWithoutAnswer("CREATE TABLE HeroStats(skillName VARCHAR (128), skillLvl int, statvalue float, UpgradeCost float)");
        GenerateValuesForDB("Strength", 1, 0.004f, 50);
        GenerateValuesForDB("MaxHP", 100, 0.003f, 50);
        GenerateValuesForDB("MaxStamina", 100, 0.003f, 50);
        GenerateValuesForDB("MaxMana", 100, 0.003f, 50);
        GenerateValuesForDB("Speed", 1f, 0.06f, 50);
        GenerateValuesForDB("CritChance", 0.05f, 0.002627f, 50);
        GenerateValuesForDB("CritDamage", 1f, 0.004f, 50);
        GenerateValuesForDB("Intellegence", 1, 0.004f, 50);
        GenerateValuesForDB("Haste", 1, 0, 50);
       // GenerateValuesForDB("HeroExpCon", 100, 2, 250);
    }

    //consIncrease - увеличение стоимости в %
    //valueIncrease - увеличение значения статистики в еденицах
    static public void GenerateValuesForDB(string skillName, float BaseValue, float valueIncrease, float MaxLvl)
    {
        float value = 0;
        for (int i = 0; i < MaxLvl; i++)
        {
            if (i == 0)
            {
                string BaseValues = BaseValue.ToString(CultureInfo.InvariantCulture);
                DbHelper.ExecuteWithoutAnswer($"INSERT INTO HeroStats VALUES('{skillName}', {i}, {BaseValues})");
            }
            else
            {
                float curValue = float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT statvalue FROM HeroStats WHERE skillLvl = {i - 1} AND skillName = '{skillName}'"));
                //Тут формулы
                switch (skillName)
                {
                    case "MaxHp":
                        value = curValue * (1.15f - (i * valueIncrease));
                        break;
                    case "MaxStamina":
                        value = curValue * (1.15f - (i * valueIncrease));
                        break;
                    case "MaxMana":
                        value = curValue * (1.15f - (i * valueIncrease));
                        break;
                    case "Speed":
                        value = Mathf.Sqrt(i) - (i * valueIncrease);
                        break;
                    case "CritChance":
                        value = curValue * (1.132f - (i * valueIncrease));
                        break;
                    case "CritDamage":
                        value = Mathf.Sqrt(i) - (i * valueIncrease);
                        break;
                    case "Strength":
                        value = Mathf.Sqrt(i) - (i * valueIncrease);
                        break;
                    case "Intellegence":
                        value = Mathf.Sqrt(i) - (i * valueIncrease);
                        break;
                    case "Haste":
                        value = 1 / Mathf.Sqrt(i);
                        break;
                    //case "HeroExpCon":
                    //    if ((i == 1) || (i == 2)) value = 100;
                    //    else value = Mathf.Log(curValue, valueIncrease) * i + curValue;
                    //    break;

                    default: break;
                }


                string valueStr = value.ToString(CultureInfo.InvariantCulture);
                DbHelper.ExecuteWithoutAnswer($"INSERT INTO HeroStats VALUES('{skillName}', {i}, {valueStr})");

            }
        }
    }

    static public void GenerateLvl(int maxLvl, float costIncreace, float baseCost)
    {
        for (int i = 0; i < maxLvl; i++)
        {
            if ((i == 0)||( i == 1 )|| (i == 2 ))
            {
                DbHelper.ExecuteWithoutAnswer($"INSERT INTO HeroLvl VALUES({i}, {baseCost})");
            }
            else
            {
                float prevCost = float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT UpgradeCost FROM HeroLvl WHERE Lvl = {i - 1}"));
                DbHelper.ExecuteWithoutAnswer($"INSERT INTO HeroLvl VALUES({i}, {(Mathf.Log(prevCost, costIncreace) * i + prevCost).ToString(CultureInfo.InvariantCulture)})");
            }
        }
    }

}


                 

        
   
