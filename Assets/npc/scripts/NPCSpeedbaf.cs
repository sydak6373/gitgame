using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeedbaf : MonoBehaviour
{
    private NPCScript npc;
    void Start()
    {
        npc = GetComponent<NPCScript>();
        npc.OnNPCAktions += Speedbaf;
    }

    
    private void Speedbaf()
    {
        Movement.currentMovementSpeed = 2f;
    }
}
