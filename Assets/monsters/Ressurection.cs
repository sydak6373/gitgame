using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressurection : State
{
    public Ressurection(Animator anim) 
    {
        this.anim = anim;
    }

    private int idealPosition;

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }

    public override void Enter() 
    {
       // UpdateAnimationResurection();

    }

    public override void LogicUpdate()
    {
        UpdateAnimationResurection();

    }

    private void UpdateAnimationResurection()
    {
        switch (idealPosition)
        {
            case 1:
                anim.Play("resLeft");
                break;
            case 2:
                anim.Play("resRight");
                break;
            case 3:
                anim.Play("resDown");
                break;
            case 4:
                anim.Play("resUp");
                break;
            default:
                anim.Play("resUp");
                break;
        }
    }
}
