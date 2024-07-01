using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFiteSkript : MonoBehaviour
{
    [SerializeField] private GameObject boneWallUp;
    [SerializeField] private GameObject boneWallDown;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Enemy somthing;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        somthing.OnDethEventUpdate += BossDeth;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        boneWallDown.SetActive(true);
        boneWallUp.SetActive(true);
        StartCoroutine(CanvasCoroutine());
        boxCollider.enabled = false;
    }

    private IEnumerator CanvasCoroutine()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
        
    }
    private void BossDeth()
    {
        
        boneWallUp.SetActive(false);
    }
}
