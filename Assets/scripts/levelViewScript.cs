using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelViewScript : MonoBehaviour
{
    
    // [SerializeField]
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = Hero.Lvl.ToString();
    }
}
