using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ChardgeEnemy : State
{
    //private int idealPosition;
    private Transform target;
    private float chardgeDictance = 1f;
    private Vector3 forward;
    private HealthInteraction health;
    public ChardgeEnemy(string enemyName, NavMeshAgent agent, Animator anim, Transform target, Transform transform, HealthInteraction health)
    {
        this.enemyName = enemyName;
        this.agent = agent;
        this.transform = transform;
        this.anim = anim;
        this.target = target;
        this.health = health;
    }

    public override void Enter()
    {
        base.Enter();

        switch (enemyName)
        {
            case "zombe": agent.speed = 1.75f; break;
            case "skelet": agent.speed = 2f; break; 
            case "Something": agent.speed = 1f + (health.maxHeatpoints - health.hitpoints)/100f; break;
            default: break;
        }
        health.OnChange += ChardgeSpeedUpdate;

    }

    public override void Exit()
    {
        
    }



    public override void LogicUpdate()
    {
         UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        
        forward = transform.position - target.position;
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
            case "zombe": BaseChardge(); break;
            case "skelet": BaseChardge(); break;
            case "Something": BaseChardge(); break;
            default: break;
        }
    }

    private void BaseChardge()
    {
        agent.SetDestination(target.position);
        //   Debug.Log("идет");
    }

    public float GetChardgeDictance()
    {  
        switch (enemyName)
        {
            case "zombe": chardgeDictance = 10f; break;
            case "skelet": chardgeDictance = 15f; break; 
            case "Something": chardgeDictance = 15f; break;
            default: break;          
        }
        return chardgeDictance;
    }

    private void ChardgeSpeedUpdate(int heatpoints, int damage)
    {
        switch (enemyName)
        {
            case "Something": agent.speed +=(-1*damage)/100f; break;
            default: break;
        }
    }
}
