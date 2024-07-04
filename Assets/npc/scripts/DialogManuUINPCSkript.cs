using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManuUINPCSkript : MonoBehaviour
{
    private NPCScript npc;
    public GameObject dialogManuUI;
    void Start()
    {
        npc = GetComponent<NPCScript>();
        npc.OnNPCAktions += Damagebaf;
    }


    private void Damagebaf()
    {
        dialogManuUI.SetActive(true);
    }
}
