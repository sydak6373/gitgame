using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon 
{
    protected Animator anim;
    protected int damage;
    protected float attakRange;
    protected Transform attakPoint;
    protected LayerMask layerMask;
    protected Transform transform;
    public Action<int> OnDamage;
    protected int idealPosition;
    protected Joystick joystick;
    public static float damageSkale;

    public virtual void atak(Animator animator, Transform transform, int idealPosition, LayerMask layerMask, Joystick joystick)
    {
        this.anim = animator;
        this.transform = transform;
        this.layerMask = layerMask;
        this.idealPosition = idealPosition;   
        this.joystick = joystick;
    }

    
}
