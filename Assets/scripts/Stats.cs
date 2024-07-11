using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;



class Stats
{
    public float Lvl { get; private set; }
    public float Strength { get; private set; }
    public float MaxHp { get; private set; }
    public float MaxStamina { get; private set; }
    public float MaxMana { get; private set; }
    public float CritChance { get; private set; }
    public float CritDamage { get; private set; }
    public float Speed { get; private set; }
    public float Intellegence { get; private set; }
    public float Haste { get; private set; }
    private List<float> RecognizedStats = new List<float>();

    public void Upgrade(string skillName, ExpHolder eh)
    {

        GetValuesFromDB db = new GetValuesFromDB();
        UpgradeStat us = new UpgradeStat();
        us.Upgrade(skillName, db.DbGetCurrentStatValue(skillName), eh, db, Lvl);
        UpdateStats();
    }

    private void RecognizeAndReturn(bool RecOrRet)
    {
        if (RecOrRet)
        {
            List<string> str = new List<string>() { "Strength", "MaxHp", "MaxStamina", "MaxMana", "CritChance", "CritDamage", "Speed", "Intellegence", "Haste", "Lvl" };
            GetValuesFromDB db = new GetValuesFromDB();
            for (int i = 0; i < RecognizedStats.Count; i++)
            {
                db.DbSetStats(str[i], RecognizedStats[i]);
            }
        }
        else
        {
            RecognizedStats = new List<float>() { Strength, MaxHp, MaxStamina, MaxMana, CritChance, CritDamage, Speed, Intellegence, Haste, Lvl };
        }
    }

    //1 этап - вызов этой штуки при начале улучшения
    public void InitializeFalseUpgrade(ExpHolder mh)
    {
        mh.RecognizeAndReturn(false);
        RecognizeAndReturn(false);
    }
    //3 этап - вызов этой штуки при клике на кнопку подтвердить или отклонить улучшение. bool accepted - true, если пользователь согласен на улучшение
    public void AcceptOrDenyFalseUpgrade(bool accepted, ExpHolder mh)
    {
        if (!accepted)
        {
            mh.RecognizeAndReturn(true);
            RecognizeAndReturn(true);
            UpdateStats();
        }
    }

    public void UpdateStats()
    {
        try
        {
            GetValuesFromDB db = new GetValuesFromDB();
            Dictionary<string, float> dic = db.DbGetHeroStats();
            Strength = dic["Strength"];
            MaxHp = dic["MaxHp"];
            MaxStamina = dic["MaxStamina"];
            MaxMana = dic["MaxMana"];
            CritChance = dic["CritChance"];
            CritDamage = dic["CritDamage"];
            Speed = dic["Speed"];
            Intellegence = dic["Intellegence"];
            Haste = dic["Haste"];
            //Добавил лвл
            Lvl = dic["Lvl"];
           
        }
        catch
        {
            try
            {
                DbHelper.ExecuteWithoutAnswer("DROP TABLE HeroCurrentStats");
               
            }
            catch { }//Таблицы не существует, нечего удалять и делать в кэтче нечего тоже
          
            DbHelper.ExecuteWithoutAnswer("CREATE TABLE HeroCurrentStats (skillName VARCHAR(128), skillLvl int, statvalue float) ");
            List<string> skillNames = new List<string>() { "Strength", "MaxHp", "MaxStamina", "MaxMana", "CritChance", "CritDamage", "Speed", "Intellegence", "Haste" };
            for (int i = 0; i < skillNames.Count; i++)
            {
                string val = float.Parse(DbHelper.ExecuteQueryWithAnswer($"SELECT statvalue FROM HeroStats WHERE skillLvl = 0 AND skillName = '{skillNames[i]}'")).ToString(CultureInfo.InvariantCulture);
                DbHelper.ExecuteWithoutAnswer($"INSERT INTO HeroCurrentStats VALUES('{skillNames[i]}', 0, {val})");
            }
            DbHelper.ExecuteWithoutAnswer($"INSERT INTO HeroCurrentStats VALUES({"'Lvl'"}, 0, 0)");
        }
    }
}