//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovmentEnemy : State
{
    public delegate void IdealPositionUpdated(int position);
    public event IdealPositionUpdated OnIdealPositionUpdated;
    private float roamingDistanceMax = 5f;
     private float roamingDistanceMin = 2f;
     private Vector3 roamPosition;
     private Vector3 startingPosition;
     private Vector3 getRandomDir;
    private int idealPosition = 1;

    public MovmentEnemy(string enemyName, NavMeshAgent agent, Animator anim, Transform transform)
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
        switch (enemyName)
        {
            case "zombe": agent.speed = 1f; break;
            case "skelet": agent.speed = 1.25f; break;
            case "Something": agent.speed = 1f; break;
            default: break;
        }

        roamPosition = GetRoamingPosition();
        //Debug.Log(roamPosition);
        //Debug.Log(startingPosition);
    }

    public override void HandleInput()
    {
        
    }

    public override void LogicUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {


        if ((getRandomDir.y < 0.5) && (getRandomDir.y > -0.5) && (getRandomDir.x > 0))
        { 
            anim.Play("movmentLeft");
            idealPosition = 1;
        }
        else if ((getRandomDir.y < 0.5) && (getRandomDir.y > -0.5) && (getRandomDir.x < 0))
        { 
            anim.Play("movmentRight");
            idealPosition = 2;
        }
        else
        {
            if (getRandomDir.y < 0) { anim.Play("movmentDown"); idealPosition = 3; }
            if (getRandomDir.y > 0) { anim.Play("movmentUp"); idealPosition = 4; }
        }
        OnIdealPositionUpdated?.Invoke(idealPosition);
    }

    public override void PhysicsUpdate()
    {
        
       agent.SetDestination(roamPosition);
    }
    private Vector3 GetRoamingPosition() 
    {
        return startingPosition + GetRandomDir() * Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private Vector3 GetRandomDir()
    {
        getRandomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        return getRandomDir;
    }
}
