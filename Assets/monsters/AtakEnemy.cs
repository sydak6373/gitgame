using System; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.AI; 
 
public class AtakEnemy : State 
{ 
    public delegate void AtakUpdated(bool atakUpdate); 
   public event AtakUpdated OnAtakUpdated; 
 
    public delegate void AtakDistance(GameObject lanse, Transform _transform); 
    public event AtakDistance AtakDistanceUpdate; 
 
    private bool atakCondition; 
    private Transform target; 
    private bool atakUpdate; 
    private float prepAtakTimer; 
    private float atakTimer; 
    private LayerMask layerMask; 
    private bool isAnimation; 
    private Vector3 forward; 
    public Action<int> OnDamage; 
    private Transform atakposition; 
    private int atakCouner; 
    private float atakDuration = 1f; 
    private float atakTime; 
    private int atakNumber =0 ; 
    private Vector3 dashPosition; 
     
 
 
    public AtakEnemy(string enemyName, NavMeshAgent agent, Animator anim, Transform target, Transform transform, LayerMask layerMask) 
    { 
        this.enemyName = enemyName; 
        this.agent = agent; 
        this.transform = transform; 
        this.anim = anim; 
        this.target = target; 
        this.layerMask = layerMask; 
        atakCondition = false; 
    } 
 
    public override void Enter() 
    { 
        base.Enter(); 
        isAnimation = true; 
        atakCondition = false; 
        atakUpdate = false; 
        
        prepAtakTimer = 0; 
        atakTimer = 0; 
        atakCouner = 0; 
        switch (enemyName) 
        { 
            case "zombe": atakTime = 1.5f; break; 
            case "skelet": atakTime = 0.83f; 
                agent.SetDestination(transform.position); break; 
            case "Something": atakTime = 1.5f;  
               if(atakNumber != 3) agent.SetDestination(transform.position);  
                break; 
            default: break; 
        } 
         
    } 
    public override void Exit() {  
        base.Exit(); 
        //Debug.Log("exit"); 
        //OnAtakUpdated?.Invoke(true); 
        atakCouner = 0; 
        atakNumber = 0; 
         
         
    } 
 
 
    public override void LogicUpdate() 
    { 
        UpdateAnimation(); 
    } 
 
    private void UpdateAnimation() 
    { 
 
        if (isAnimation) { 
            isAnimation = false; 
            if (atakUpdate) 
            { 
                if(atakNumber == 3) 
                { 
                     
 
                    if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0)) 
                    { 
                        anim.Play("movmentRight"); 
                        //idealPosition = 1; 
                    } 
                    else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0)) 
                    { 
                        anim.Play("movmentLeft"); 
                        //idealPosition = 2; 
                    } 
                    else 
                    { 
                        if (forward.y < 0) { anim.Play("movmentUp"); } 
                        if (forward.y > 0) { anim.Play("movmentDown"); } 
                    } 
 
                } 
                else if (atakNumber == 2)  
                { 
                    if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0)) 
                    { 
                        anim.Play("atakRight 0"); 
                        //idealPosition = 1; 
                    } 
                    else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0)) 
                    { 
                        anim.Play("atakLeft 0"); 
                        //idealPosition = 2; 
                    } 
                    else 
                    { 
                        if (forward.y < 0) { anim.Play("atakUp 0"); } 
                        if (forward.y > 0) { anim.Play("atakDown 0"); } 
                    } 
                } 
                else 
                { 
                    if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0)) 
                    {
                      anim.Play("atakRight"); 
                        //idealPosition = 1; 
                    } 
                    else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0)) 
                    { 
                        anim.Play("atakLeft"); 
                        //idealPosition = 2; 
                    } 
                    else 
                    { 
                        if (forward.y < 0) { anim.Play("atakUp"); } 
                        if (forward.y > 0) { anim.Play("atakDown"); } 
                    } 
                } 
            } 
            else 
            { 
                forward = transform.position - target.position; 
                forward = forward.normalized; 
                if ((atakNumber == 2)||(atakNumber == 3)) 
                { 
                    
 
                    if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0)) 
                    { 
                        anim.Play("RangePrepAtakRight"); 
                        //idealPosition = 1; 
                    } 
                    else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0)) 
                    { 
                        anim.Play("RangePrepAtakLeft"); 
                        //idealPosition = 2; 
                    } 
                    else 
                    { 
                        if (forward.y < 0) { anim.Play("RangePrepAtakUp"); } 
                        if (forward.y > 0) { anim.Play("RangePrepAtakDown"); } 
                    } 
                     
 
                } 
                else 
                { 
                     
 
                    if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0)) 
                    { 
                        anim.Play("prepAtakRight"); 
                        //idealPosition = 1; 
                    } 
                    else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0)) 
                    { 
                        anim.Play("prepAtakLeft"); 
                        //idealPosition = 2; 
                    } 
                    else 
                    { 
                        if (forward.y < 0) { anim.Play("prepAtakUp"); } 
                        if (forward.y > 0) { anim.Play("prepAtakDown"); } 
                    } 
                    
 
                    // Debug.Log(atakposition); 
                } 
                atakposition = AtakPosition(); 
            } 
        } 
    } 
 
    public override void PhysicsUpdate() 
    { 
        BasePrepAtak(); 
    } 
    public void OnTriggerEnter2D(Collision2D collision) 
    { 
       // Debug.Log(atakNumber); 
        if (atakNumber == 3) 
        { 
            agent.acceleration = 8; 
            //Debug.Log("           "); 
            if (collision.gameObject.GetComponent<HealthInteraction>()) 
            { 
 
                switch (enemyName) 
                { 
                    case "Something": 
                        collision.gameObject.GetComponent<HealthInteraction>().Change(-45); 
                        OnAtakUpdated?.Invoke(atakUpdate); 
                        //transform.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false; 
                        break; 
 
                    default: break; 
                } 
            } 
        } 
    } 
 
    public void BasePrepAtak() 
    { 
        prepAtakTimer += Time.deltaTime; 
        //Debug.Log(atakposition); 
        if (prepAtakTimer >= atakTime)  
        { 
            //Debug.Log("     "); 
            prepAtakTimer = 0; 
            switch (enemyName) 
            { 
                case "zombe": ZombeAtak();  break; 
                case "skelet": SkeletAtak(); break; 
                case "Something": SomethingAtak(); break; 
                default: break; 
            } 
 
        } 
        if (atakUpdate) 
        { 
 
            switch (enemyName) 
            { 
                case "zombe": atakDuration = 0.2475f; break; 
                case "skelet": 
                    if (atakNumber == 1) 
                    {
if (atakCouner == 0) atakDuration = 0.2475f; 
                        else atakDuration = 0.75f; 
                    } 
                    else 
                    { 
                        atakDuration = 2f; 
                    } 
                    break; 
                case "Something": 
                    if (atakNumber == 1) 
                    { 
                        atakDuration = 1f; 
                    } 
                    else if (atakNumber == 2) 
                    { 
                        atakDuration = 3f; 
                    } 
                    else 
                    { 
                        atakDuration =6f; 
 
                    } 
                    break; 
                default: break; 
            } 
 
            atakTimer += Time.deltaTime; 
            switch (enemyName) 
            { 
                case "Something": 
                    if ((atakTimer >= atakDuration)|| (transform.position == dashPosition)) { 
                        atakNumber = 0; 
                        atakTimer = 0; 
                        OnAtakUpdated?.Invoke(atakUpdate); 
                         
                    } 
                    break; 
                default:  
                    if ((atakTimer >= atakDuration)) OnAtakUpdated?.Invoke(atakUpdate);  
                    break; 
            } 
        } 
 
    } 
 
    private Transform AtakPosition() 
    { 
        if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x > 0)) 
        { 
            //Debug.Log(transform.GetChild(3)); 
            return transform.GetChild(3); 
        } 
        else if ((forward.y < 0.5) && (forward.y > -0.5) && (forward.x < 0)) 
        { 
           // Debug.Log(transform.GetChild(4)); 
            return transform.GetChild(4); 
            
        } 
        else 
        { 
            if (forward.y < 0)  
            {  
                //Debug.Log(transform.GetChild(2));  
                return transform.GetChild(2); }   
            if (forward.y > 0)  
            {  
               // Debug.Log(transform.GetChild(1));  
                return transform.GetChild(1); } 
             
        } 
        return transform.GetChild(4); 
 
    } 
    private void ZombeAtak() 
    { 
        atakUpdate = true; 
        isAnimation = true; 
 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atakposition.position, 0.7f, layerMask); 
       // Debug.Log(atakposition); 
        foreach (Collider2D hit in hitEnemies) { 
            //Debug.Log(hit.gameObject); 
            hit.gameObject.GetComponent<HealthInteraction>().Change(-50); 
            OnDamage?.Invoke(-50); 
            
        } 
        
         
    } 
 
    private void SomethingAtak() 
    { 
        
        isAnimation = true; 
        if (atakNumber == 1) 
        { 
            Debug.Log("atak1"); 
            atakUpdate = true; 
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atakposition.position, 1.5f, layerMask); 
            // Debug.Log(atakposition); 
            foreach (Collider2D hit in hitEnemies) 
            { 
                //Debug.Log(hit); 
                hit.gameObject.GetComponent<HealthInteraction>().Change(-60); 
                OnDamage?.Invoke(-60); 
 
            } 
 
        } 
        else if (atakNumber == 2) 
        { 
            Debug.Log("atak2"); 
            atakUpdate = true; 
                AtakDistanceUpdate?.Invoke(transform.GetChild(6).gameObject, transform); 
         } 
        else if(atakNumber == 3) 
        { 
            Debug.Log("atak3"); 
            if (!atakUpdate) 
                { 
                    Vector3 forwardDash = transform.position - target.position; 
                    forwardDash = forwardDash.normalized; 
                    dashPosition = transform.position + (forwardDash * -20); 
                    Debug.Log("         " + dashPosition); 
                    atakUpdate = true; 
                    //transform.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
agent.acceleration = 100; 
                    agent.speed = 3f + (transform.gameObject.GetComponent<HealthInteraction>().maxHeatpoints - transform.gameObject.GetComponent<HealthInteraction>().hitpoints) / 25f; 
                    agent.SetDestination(dashPosition); 
 
                } 
        } 
         
    } 
 
    private void SkeletAtak() 
    { 
         
        if (atakNumber == 1) 
        { 
            atakUpdate = true; 
            isAnimation = true; 
            Vector2 size = new Vector2(1.7f, 0.5f); 
            Collider2D[] hitEnemies; 
            if ((atakposition == transform.GetChild(3)) || (atakposition == transform.GetChild(4))) hitEnemies = Physics2D.OverlapCapsuleAll(atakposition.position, size, CapsuleDirection2D.Horizontal, 0.5f, layerMask); 
            else hitEnemies = Physics2D.OverlapCapsuleAll(atakposition.position, size, CapsuleDirection2D.Vertical, 0.5f, layerMask); 
            // Debug.Log(atakposition); 
            foreach (Collider2D hit in hitEnemies) 
            { 
                //Debug.Log(hit); 
                hit.gameObject.GetComponent<HealthInteraction>().Change(-15); 
                OnDamage?.Invoke(-15); 
                if ((atakCouner < 2)) 
                { 
                    atakCouner++; 
                    atakTime = 0.25f; 
 
 
                } 
 
            } 
 
        } else 
        { 
            atakUpdate = true; 
            isAnimation = true; 
             
            
            AtakDistanceUpdate?.Invoke(transform.GetChild(6).gameObject, transform); 
           atakNumber = 0;  
        } 
    } 
 
    public bool AtakConditionUpdated() 
    { 
         
            switch (enemyName) 
            { 
                case "zombe": BaseCondition(1.4f); break; 
                case "skelet": BaseCondition(7.5f, 1.7f); break; 
                case "Something": BaseCondition(10f, 3f); break; 
                // case "Something": BaseCondition(3f); break; 
                default: break; 
            } 
         
        return atakCondition; 
    } 
 
    private void BaseCondition(float atakRange) 
    { 
        if (Vector3.Distance(transform.position, target.position) < atakRange) { atakCondition = true; } 
        else atakCondition = false; 
    } 
 
    private void BaseCondition(float atakRange1, float atakRange2) 
    { 
        if(atakNumber == 0) { 
            if (Vector3.Distance(transform.position, target.position) < atakRange2) 
            { 
                atakCondition = true; 
                if (atakNumber != 3) atakNumber = 1; 
            } 
            else if (Vector3.Distance(transform.position, target.position) < atakRange1) 
            { 
                atakCondition = true; 
 
                switch (enemyName) 
                { 
                    case "skelet": atakTime = 2f; atakNumber = 2; break; 
                    case "Something": 
 
                        float atakNumberUpdate = UnityEngine.Random.Range(0, 2); 
                        //Debug.Log(atakNumber + "atakNumberUpdate" + atakNumberUpdate); 
                        if (atakNumberUpdate >= 1) { if (atakNumber != 3) { atakNumber = 2; atakTime = 5f; } } 
                        else { if (atakNumber != 2) { atakNumber = 3; atakTime = 1f; } } 
 
                        break; 
                    default: break; 
                } 
            } 
            else  atakCondition = false; 
        } 
    } 
 
}
