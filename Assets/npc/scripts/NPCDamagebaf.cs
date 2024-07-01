using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDamagebaf : MonoBehaviour
{
    private NPCScript npc;
    void Start()
    {
        npc = GetComponent<NPCScript>();
        npc.OnNPCAktions += Damagebaf;
    }


    private void Damagebaf()
    {
        Weapon.damageSkale = 1.5f;
    }
}
