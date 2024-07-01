using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : State
{
    private int idealPosition;
    private SpriteRenderer sprite;
    private string damageColor = "#FF5353";
    private Color color;
    
    public TakingDamage(Animator anim, SpriteRenderer sprite)
    {
        this.anim = anim;
        this.sprite = sprite;
    }

    public override void Enter()
    {
        base.Enter();
        ColorUtility.TryParseHtmlString(damageColor, out color);
        sprite.color = color;
    }

    public override void Exit()
    {
        base.Exit();
        sprite.color = Color.white;
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
                anim.Play("idealRight");
                break;
            case 2:
                anim.Play("idealLeft");
                break;
            case 3:
                anim.Play("idealDown");
                break;
            default:
                anim.Play("ideal");
                break;
        }

    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }

}
