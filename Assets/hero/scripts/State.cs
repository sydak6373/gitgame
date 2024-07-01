using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected Rigidbody2D rigidB;
    protected Animator anim;
    protected Transform transform;
    protected CapsuleCollider2D capsCol;
    protected string enemyName;
    protected NavMeshAgent agent;

    public State() { }

    public State(Rigidbody2D rigidB, Animator anim, Transform transform)
    {
        this.rigidB = rigidB;
        this.anim = anim;
        this.transform = transform;
        
    }

    public State(Rigidbody2D rigidB, Animator anim, Transform transform, CapsuleCollider2D capsCol)
    {
        this.rigidB = rigidB;
        this.anim = anim;
        this.transform = transform;
        this.capsCol = capsCol;
    }

    public State(string enemyName, Rigidbody2D rigidB, Animator anim, Transform transform, CapsuleCollider2D capsCol)
    {
        this.rigidB = rigidB;
        this.anim = anim;
        this.transform = transform;
        this.capsCol = capsCol;
        this.enemyName = enemyName;
    }

    public State(string enemyName, NavMeshAgent agent, Rigidbody2D rigidB, Animator anim, Transform transform, CapsuleCollider2D capsCol)
    {
        this.agent = agent;
        this.rigidB = rigidB;
        this.anim = anim;
        this.transform = transform;
        this.capsCol = capsCol;
        this.enemyName = enemyName;
    }

    virtual public void Enter() { }
    virtual public void Exit() { }
    virtual public void HandleInput() { }
    virtual public void LogicUpdate() { }
    virtual public void PhysicsUpdate() { }
}




