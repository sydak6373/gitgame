using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpHolder : MonoBehaviour
{
    public float exp { get; set; }
    private float recognizedExp;
    public void DecreaseExp(float cost)
    {
        if (exp >= 0)
        {
            exp -= cost;
        }
    }
    public void RecognizeAndReturn(bool RecOrRet)
    {
        if (RecOrRet)
        {
            exp = recognizedExp;
            recognizedExp = 0;
        }
        else
        {
            recognizedExp = exp;
        }
    }
}
