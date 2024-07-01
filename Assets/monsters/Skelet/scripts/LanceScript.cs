using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D ridgidbadyLanse;
    [SerializeField] private Transform target;
    private Vector3 forward;
    private string enemyName;
    void Start()
    {
        forward =  target.position - transform.position;
        forward = forward.normalized;
        ridgidbadyLanse.velocity = forward * speed;
        enemyName = gameObject.tag;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthInteraction>()) {
            
            switch (enemyName)
            {
                case "skelet": collision.gameObject.GetComponent<HealthInteraction>().Change(-15); break;
                case "Something": collision.gameObject.GetComponent<HealthInteraction>().Change(-35); break;
               
                default: break;
            }
        }
        Destroy(this.gameObject);
    }
}
