using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DethState : State
{
    public static Action deth;
    private int idealPosition;
    public DethState(Animator anim)
    {
        this.anim = anim;   
    }

    public override void Enter()
    {
        base.Enter();
        deth?.Invoke();
    }
    public override void LogicUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {

        switch (idealPosition)
        {
            case 1:
                anim.Play("DethRight");
                break;
            case 2:
                anim.Play("DethLeft");
                break;
            case 3:
                anim.Play("DethDown");
                break;
            default:
                anim.Play("DethUp");
                break;
        }

    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }

     
}
