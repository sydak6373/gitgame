using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private Transform play;
    [SerializeField] private HealthInteraction healt;
    private Vector3 pos;
    public float _duration = .3f;
   
    private Vector3 _originalPosition;

    private void Awake()
    {
        if (!play) { play = FindObjectOfType<Hero>().transform; }
       
        

        healt.OnChange += Shake;
    }

    private void Start()
    {
        transform.position = new Vector3(play.transform.position.x, play.transform.position.y, transform.position.z);
    }

    public void Shake(int hp, int damage)
    {
        _originalPosition = transform.position;
        if (damage < 0) StartCoroutine(_Shake());
    }

    IEnumerator _Shake()
    {

        float x;
        float y;
        float timeLeft = Time.time;

        while ((timeLeft + _duration) > Time.time)
        {
            x = Random.Range(-0.3f, 0.3f);
            y = Random.Range(-0.3f, 0.3f);

            transform.position = new Vector3(transform.position.x+x, transform.position.y+y, _originalPosition.z); yield return new WaitForSeconds(0.025f);
        }

        transform.position = _originalPosition;

    }


    void Update()
    {
        pos = play.position;
        pos.z = play.position.z - 10f;
       
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
       

    }
}
