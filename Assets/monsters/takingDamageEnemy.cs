using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TakingDamageEnemy : State
{
    public TakingDamageEnemy(string enemyName, NavMeshAgent agent, Rigidbody2D rigidB, Animator anim, Transform transform, CapsuleCollider2D capsCol) : base(enemyName, agent, rigidB, anim, transform, capsCol)
    {
    }
}
