using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int heatpoints;
    public int maxHeatpoints;
    public int curentMana;
    public int maxMana;
    public int curentStamina;
    public int maxStamina;
    public int HeroLvl;
    public int Exp;

    public float[] position;
    public float Streinght;
    public float Intelegens;
    public float speed;
    public float critDamage;
    public float critShance;



    public PlayerData()
    {
        position = new float[3];
        heatpoints = 100;
        maxHeatpoints = 100;
        curentMana = 100;
        maxMana = 100;
        curentStamina = 100;
        maxStamina = 100;

        this.position[0] = 0;
        this.position[1] = 0;
        this.position[2] = 0;

        speed = Movement.currentMovementSpeed;
        Streinght = Weapon.damageSkale;
    }

    


    public PlayerData( int maxHP,  int maxMp,  int maxSt, Vector3 position)
    {
        this.position = new float[3];
        this.heatpoints = maxHP;
        maxHeatpoints = maxHP;
        curentMana = maxMp;
        maxMana = maxMp;
        curentStamina = maxSt;
        maxStamina = maxSt;
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
        speed = Movement.currentMovementSpeed;
        Streinght = Weapon.damageSkale;
    }

    public PlayerData(int maxHP, int maxMp, int maxSt)
    {
        this.position = new float[3];
        this.heatpoints = maxHP;
        maxHeatpoints = maxHP;
        curentMana = maxMp;
        maxMana = maxMp;
        curentStamina = maxSt;
        maxStamina = maxSt;

        speed = Movement.currentMovementSpeed;
        Streinght = Weapon.damageSkale;
        
    }
}
