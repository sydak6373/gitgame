using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DethEnemy : State
{
    public delegate void isResurectionUpdate(bool isResur);
    private GameObject gameObject;
    private Vector3 position;
    private float resurrectionTimer;
    private int isResurrection = 0;
    private bool resurection = false;
    private int idealPosition;
   
    public event isResurectionUpdate OnIsResurectionUpdate;

    public DethEnemy(GameObject gameObject, string enemyName, Animator anim)
    {
        this.enemyName = enemyName;
        this.gameObject = gameObject;
        this.anim = anim;
    }


    public override void Enter()
    {
        base.Enter();
        switch (enemyName)
        {
            case "zombe": ZombeEnter(); break;
            case "skelet": BaseDeth(); break;
            case "Something": BaseDeth(); break;
            default: break;
        }

    }

    public override void LogicUpdate()
    {

        gameObject.transform.position = position;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        switch (enemyName)
        {
            case "zombe": ZombePhysicsUpdate(); break;
            
            default: break;
        }
    }

    private void ZombeEnter()
    {
        resurrectionTimer = 0f;
        BaseDeth();
    }

    private void ZombePhysicsUpdate()
    {
        if (!resurection)
        {
            if (resurrectionTimer <= 15f)
            {
                resurrectionTimer += Time.deltaTime;

                if (resurrectionTimer > 15f)
                {

                    isResurrection = Random.Range(0, 2);
                    //Debug.Log(isResurrection);
                    if (isResurrection == 1)
                    {
                        //Debug.Log("Воскрес");
                        gameObject.layer = LayerMask.NameToLayer("enemy");
                       // gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                        resurection = true;
                        gameObject.GetComponent<HealthInteraction>().Change(20);
                        
                        OnIsResurectionUpdate?.Invoke(resurection);

                    }
                }
            }
        }


    }

    private void BaseDeth()
    {
        position = gameObject.transform.position;
        gameObject.layer = LayerMask.NameToLayer("death");
        // gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        UpdateAnimationDeath();
    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }

    private void UpdateAnimationDeath()
    {
        switch (idealPosition)
        {
            case 1:
                anim.Play("deathLeft");
                break;
            case 2:
                anim.Play("deathRight");
                break;
            case 3:
                anim.Play("deathDown");
                break;
            case 4:
                anim.Play("deathUp");
                break;
            default:
                anim.Play("deathUp");
                break;
        }
    }

   
}

