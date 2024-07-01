using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class RemuveEnemy : State
{
    private bool isRemuve = true;
    private Vector3 remuvePosition;
    private Vector3 forward;
    public RemuveEnemy(string enemyName, NavMeshAgent agent, Animator anim, Transform transform)
    {
        this.enemyName = enemyName;
        this.agent = agent;
        this.transform = transform;
        remuvePosition = transform.position;
        this.anim = anim;
    }
    public override void Enter()
    {
        base.Enter();
        isRemuve = false;
        switch (enemyName)
        {
            case "zombe": agent.speed = 1f; break;
            case "skelet": agent.speed = 1.5f; break;
            case "Something": agent.speed = 0.85f; break;
            default: break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        isRemuve = true;

    }

    public override void LogicUpdate()
    {
        UpdateAnimation();
        
    }
    private void UpdateAnimation()
    {

        forward = transform.position - remuvePosition;
        forward = forward.normalized;
        //Debug.Log(forward);

        if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0))
        {
            anim.Play("movmentRight");
            //idealPosition = 1;
        }
        else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0))
        {
            anim.Play("movmentLeft");
            //idealPosition = 2;
        }
        else
        {
            if (forward.y < 0) { anim.Play("movmentUp"); }
            if (forward.y > 0) { anim.Play("movmentDown"); }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        switch (enemyName)
        {
            case "zombe": BaseRemuve(); break;
            case "skelet": BaseRemuve(); break; 
            case "Something": BaseRemuve(); break;
            default: break;
        }
    }

    private void BaseRemuve()
    {
        agent.SetDestination(remuvePosition);
        //Debug.Log("rem" + remuvePosition);
        //Debug.Log("cur" + transform.position);
        //   Debug.Log("идет");
        //if ((transform.position.x == remuvePosition.x)&& (transform.position.y == remuvePosition.y)) { isRemuve = true; }
    }

    public bool GetIsRemuve() 
    {
        if (Vector3.Distance(transform.position, remuvePosition) < 0.5f) { isRemuve = true; }
        //Debug.Log(Vector3.Distance(transform.position, remuvePosition) < 0.5f);
       // Debug.Log(isRemuve);
        return isRemuve; 
    }

}
