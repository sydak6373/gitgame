using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int heatpoints;
    public int maxHeatpoints;
    public float[] position;
    public float damageSkaler;
    public float speed;

    public PlayerData()
    {
        position = new float[3];
        heatpoints = 100;
        maxHeatpoints = 100;
        this.position[0] = 0;
        this.position[1] = 0;
        this.position[2] = 0;
        speed = Movement.currentMovementSpeed;
        damageSkaler = Weapon.damageSkale;
    }

    public PlayerData(int i)
    {
        position = new float[3];
        heatpoints = 100;
        maxHeatpoints = 100;
        this.position[0] = 0;
        this.position[1] = 0;
        this.position[2] = 0;
        speed = Movement.currentMovementSpeed;
        damageSkaler = Weapon.damageSkale;
    }


    public PlayerData(int heatpoints, int maxHP, Vector3 position)
    {
        this.position = new float[3];
       this.heatpoints = heatpoints;
        maxHeatpoints = maxHP;
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
        speed = Movement.currentMovementSpeed;
        damageSkaler = Weapon.damageSkale;
    }
}
