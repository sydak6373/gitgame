using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject panel;
    void Start()
    {
        
    }

    
    void Update()
    {
        dialogUI.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        panel.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);

    }

    public void DialogInteractions(string text)
    {
        dialogUI.GetComponentInChildren<Text>().text = text;
        dialogUI.SetActive(true);
    }

    public void DialogInteractions()
    {
        
        dialogUI.SetActive(false);
    }
}
