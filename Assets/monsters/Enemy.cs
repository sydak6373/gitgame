using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static DethEnemy;

public class Enemy : MonoBehaviour
{
    private string enemyName;
    private bool isLive = true;
    private Rigidbody2D rigidB;
    private Animator anim;
    private CapsuleCollider2D capsCol;
    private StateMashine stateMashine;
    [SerializeField] private GameObject target;
    [SerializeField] private float roamingTimerMax = 1f;
    [SerializeField] private float roamingTimerMin = 0.5f;
    [SerializeField] private LayerMask layerMask;
    //[SerializeField] private UnityEvent trigered;
    private float roamingTimerCurrent = 2f;
    private float chasingRange;
    private float distanceToPlayer;
    private bool isAtak = true;

    private float roamingTime;
    private float resTimer;
    HealthInteraction healt;

    private NavMeshAgent agent;

    private MovmentEnemy movmentEnemy;
    private IdealEnemy idealEnemy;
    private DethEnemy dethEnemy;
    private Ressurection ressurection;
    private ChardgeEnemy chardgeEnemy;
    private RemuveEnemy remuveEnemy;
    
    private AtakEnemy atakEnemy;
    private TakingDamageEnemy takingDamageEnemy;

    public delegate void DethEvent();
    public event DethEvent OnDethEventUpdate;

    private void Awake()
    {
        healt = GetComponent<HealthInteraction>();
        enemyName = gameObject.tag;
        rigidB = GetComponent<Rigidbody2D>();
        anim = rigidB.GetComponent<Animator>();
        capsCol = GetComponent<CapsuleCollider2D>();
        agent = GetComponent<NavMeshAgent>();

        stateMashine = new StateMashine();

        movmentEnemy = new MovmentEnemy(enemyName, agent , anim, transform);
        idealEnemy = new IdealEnemy(enemyName, agent, anim, transform);
        chardgeEnemy = new ChardgeEnemy(enemyName, agent, anim, target.GetComponent<Transform>(), transform, healt);
        remuveEnemy = new RemuveEnemy(enemyName, agent, anim, transform);
        dethEnemy = new DethEnemy(gameObject, enemyName, anim);
        takingDamageEnemy = new TakingDamageEnemy(enemyName, agent, rigidB, anim, transform, capsCol);
        ressurection = new Ressurection(anim);
        
        atakEnemy = new AtakEnemy(enemyName, agent, anim, target.GetComponent<Transform>(), transform, layerMask);

        roamingTime = roamingTimerCurrent;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        stateMashine.SetState(idealEnemy);

        movmentEnemy.OnIdealPositionUpdated += UpdateIdealPosition;
        healt.OnChange += DamageAndDeth;
        dethEnemy.OnIsResurectionUpdate += isLiveUpdate;
        atakEnemy.OnAtakUpdated += IsAtakUpdate;
        atakEnemy.AtakDistanceUpdate += isAtakDistanceUpdate;

        chasingRange = chardgeEnemy.GetChardgeDictance();

        resTimer = 1f;
    }

    private void Update()
    {
       // agent.SetDestination(target.position);
        stateMashine.CurrentState.HandleInput();
        stateMashine.CurrentState.LogicUpdate();
        distanceToPlayer = Vector3.Distance(transform.position, target.GetComponent<Transform>().position);

    }

    private void FixedUpdate()
    {

       stateMashine.CurrentState.PhysicsUpdate();
        if (isLive && (resTimer >= 1f))
        {
            if(atakEnemy.AtakConditionUpdated())
            {
                //Debug.Log(prepAtakEnemy.AtakConditionUpdated());
                if (isAtak)
                {
                    isAtak = false;
                    stateMashine.SetState(atakEnemy);
                    Debug.Log("atak");
                }
                

            }
            else if (distanceToPlayer <= chasingRange && isAtak)
            {
               if (stateMashine.CurrentState != chardgeEnemy)
               {
                    stateMashine.SetState(chardgeEnemy);
                    //Debug.Log("чаржит");
                }
            } else if ((distanceToPlayer >= chasingRange) && isAtak) 
            {
                if (stateMashine.CurrentState == chardgeEnemy)
                { 
                    stateMashine.SetState(remuveEnemy);
                   // Debug.Log("remuve"); 
                
                }
               // Debug.Log(remuveEnemy.GetIsRemuve());
                if ((remuveEnemy.GetIsRemuve() == true) && (stateMashine.CurrentState == remuveEnemy))
                {
                    roamingTime = 0;
                    stateMashine.SetState(idealEnemy);
                   
                   // Debug.Log("state");
                }
            }
            if((stateMashine.CurrentState == movmentEnemy)||(stateMashine.CurrentState == idealEnemy) ) {
                if (roamingTime > roamingTimerCurrent) roamingTime = roamingTimerCurrent;
                if (roamingTime == roamingTimerCurrent)
                {
                    stateMashine.SetState(movmentEnemy);
                   // Debug.Log("идет");

                }
                if (stateMashine.CurrentState == movmentEnemy) roamingTime -= Time.deltaTime;
                if (roamingTime <= 0)
                {
                    stateMashine.SetState(idealEnemy);
                    roamingTimerCurrent = Random.Range(roamingTimerMin, roamingTimerMax);
                   // Debug.Log("стоит");

                }
                if (stateMashine.CurrentState == idealEnemy) roamingTime += Time.deltaTime;
            }
        }
        else if (resTimer < 1f)
        {
            resTimer += Time.deltaTime;
            //Debug.Log(resTimer);
            if (resTimer >= 1f) stateMashine.SetState(idealEnemy);
        }
    }

    private void UpdateIdealPosition(int position)
    {
        idealEnemy.UpdateIdealPosition(position);
        dethEnemy.UpdateIdealPosition(position);
        ressurection.UpdateIdealPosition(position);
    }

    private void IsAtakUpdate(bool atakUpdate)
    {
        isAtak = atakUpdate;
    }


    private void DamageAndDeth(int hp, int damage)
    {
        if(hp <= 0) 
        { 

            stateMashine.SetState(dethEnemy); isLive = false;
            target.GetComponent<HealthInteraction>().maxHeatpoints++;
            target.GetComponent<HealthInteraction>().hitpoints++;

            Movement.currentMovementSpeed += 0.05f;
            OnDethEventUpdate?.Invoke();
        }
        else if (damage > 0) { stateMashine.SetState(idealEnemy); }
        //else if(hp > 0) { }
    }

    private void isLiveUpdate(bool isLiveUpdate)
    {
        isLive = isLiveUpdate;
        isAtak = true;
        stateMashine.SetState(ressurection);
        resTimer = 0f;
    }

    private void isAtakDistanceUpdate(GameObject bullet, Transform _transform)
    {
        
        Vector3 pos = target.transform.position - bullet.transform.GetChild(0).transform.position;
        float rotation = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        bullet.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, rotation);
        Instantiate(bullet, _transform.position, transform.rotation).SetActive(true);
        
        
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, 1.4f);
        Gizmos.DrawWireSphere(transform.GetChild(2).position, 1.5f);
        Gizmos.DrawWireSphere(transform.GetChild(1).position, 1.5f);
        Gizmos.DrawWireSphere(transform.GetChild(3).position, 1.5f);
        Gizmos.DrawWireSphere(transform.GetChild(4).position, 1.5f);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (stateMashine.CurrentState == atakEnemy)
        {
            atakEnemy.OnTriggerEnter2D(collision);
        }
    }


}

