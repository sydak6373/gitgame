using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPChethbaf : MonoBehaviour
{

    private NPCScript npc;
    void Start()
    {
        npc = GetComponent<NPCScript>();
        npc.OnNPCAktions += HPbaf;
    }


    private void HPbaf()
    {
        npc.heroHealth.maxHeatpoints = 140;
    }
}
