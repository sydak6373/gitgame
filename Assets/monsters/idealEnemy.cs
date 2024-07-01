using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdealEnemy : State
{
    private Vector3 startingPosition;
    private int idealPosition;

    public IdealEnemy(string enemyName, NavMeshAgent agent,  Animator anim, Transform transform )
    {
        this.enemyName = enemyName;
        this.agent = agent;
        this.anim = anim;
        this.transform = transform;

    }

    override public void Enter()
    {
        base.Enter();
        startingPosition = transform.position;
        //Debug.Log(startingPosition);
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
                anim.Play("idleLeft");
                break;
            case 2:
                anim.Play("idleRight");
                break;
            case 3:
                anim.Play("idleDown");
                break;
            case 4:
                anim.Play("Up");
                break;
            default:
                anim.Play("Up");
                break;
        }
    }

    public override void HandleInput()
    {
        //transform.position = startingPosition;
    }

    public override void PhysicsUpdate()
    {
       
        agent.SetDestination(startingPosition);
    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }
}
