using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
   
    public Sword() 
    {
        base.damage = -10;
        base.attakRange = 0.6f;
        damageSkale = 1f;

    }


    public override void atak(Animator animator , Transform transform, int idealPosition, LayerMask layerMask, Joystick joystick)
    {
        
        base.atak(animator, transform, idealPosition, layerMask, joystick);
        UpdateGetAxis();
        UpdateAnimation();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attakPoint.position, attakRange, base.layerMask);
       
        foreach (Collider2D hit in hitEnemies)
        {
            if (hit.gameObject.GetComponent<HealthInteraction>())
            {
                hit.gameObject.GetComponent<HealthInteraction>().Change((int)(damage * damageSkale));
                OnDamage?.Invoke((int)(damage * damageSkale));
            }
        }
         
    }

    private void UpdateGetAxis()
    {

        if ((joystick.Horizontal == 1) )
        {
            
            idealPosition = 1;


        }
        if ((joystick.Horizontal == -1))
        {
            
            idealPosition = 2;

        }
        if (joystick.Vertical == 1) 
        {
            
            idealPosition = 3;

        }
        if (joystick.Vertical == -1)
        {
            
            idealPosition = 4;
        }
        
    }

    private void UpdateAnimation()
    {
        if ((idealPosition == 1))
        {
            attakPoint = transform.Find("atakRight");
            anim.Play("swordAtakRight");


        }
        else if ((idealPosition == 2))
        {
            attakPoint = transform.Find("atakLeft");
            anim.Play("swordAtakLeft");

        }
        else if ((idealPosition == 3))
        {
            attakPoint = transform.Find("atakUp");
            anim.Play("swordAtakUp");

        }
        else if ((idealPosition == 4))
        {

            attakPoint = transform.Find("atakDown");
            anim.Play("swordAtakDown");
        }else
        {
            attakPoint = transform.Find("atakDown");
            anim.Play("swordAtakDown");

        }

    }
}
