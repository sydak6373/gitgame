using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsInteraction : State
{
    private Weapon weapon;
    private int idealPosition = 0;
    private LayerMask layerMask;
    private Joystick joystick;

    public WeaponsInteraction(Rigidbody2D rigidbody, Animator animator, Transform transform, CapsuleCollider2D capsuleCollider, Weapon weapon,  LayerMask layerMask, Joystick joystick)
        : base(rigidbody, animator, transform, capsuleCollider)
    {
        this.weapon = weapon;
        this.layerMask = layerMask;
        this.joystick = joystick;
    }

    public override void Enter() 
    {
        //Debug.Log("Сообщение для отладки");
        weapon.atak(anim, transform, idealPosition, layerMask, joystick);
;    
    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }

    public override void HandleInput()
    {
        // Handle input if needed
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
    }

   
}


