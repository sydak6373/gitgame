﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : MonoBehaviour
{
    
    public LayerMask layerMask;
    public Joystick joystick;
    public Button atakButton;
    public Button somButton;
    private Stats stats;
    public static int Lvl;

    public static float haste = 1;
    public static float Intelegent = 1;
    public static float critShance = 0;
    public static float critDAmage = 1;

    private float SomersaultCooldownSeconds = 0.5f;
    private bool isSomersaultColdawn = true;
   


    private float AtakCooldownSeconds = 0.33f;
    private bool isAtakColdawn = true;
    private float AtakDuration = 0.33f;

    private float DamageDuration = 1f;
    private bool isDamage = true;

    private bool isClicedSomersault = false;
    private bool isClicedAtak = false;

    private Rigidbody2D rigidB;
    private Animator anim;
    private CapsuleCollider2D capsCol;
    private ExpHolder expHolder;

    private HealthInteraction health;
    private ManaInteraction mana;
    private StaminaInteraction stamina;

    private SpriteRenderer sprite;

    private Movement movement;
    private StateMashine stateMashine;
    private Ideal ideal;
    private Somersault somersault;
    private WeaponsInteraction weaponsInteraction;
    private Weapon weapon;
    private TakingDamage takingDamage;
    private DethState dethState;

    private bool isSomersaulting = false;
    private bool isAttacking = false;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        anim = rigidB.GetComponent<Animator>();
        capsCol = GetComponent<CapsuleCollider2D>();

        health = GetComponent<HealthInteraction>();
        mana = GetComponent<ManaInteraction>();
        stamina = GetComponent<StaminaInteraction>();
        expHolder = GetComponent<ExpHolder>();

        sprite = GetComponentInChildren<SpriteRenderer>();

        movement = new Movement(rigidB, anim, transform, capsCol, joystick);
        somersault = new Somersault(rigidB, anim, transform, capsCol, joystick);
        ideal = new Ideal(rigidB, anim, transform, capsCol);
        weapon = new Sword();
        weaponsInteraction = new WeaponsInteraction(rigidB, anim, transform, capsCol, weapon, layerMask, joystick);
        takingDamage = new TakingDamage(anim, sprite);
        dethState = new DethState(anim);
        stats = new Stats();
        //stats = GlobalSaveAndLoad.LoadStats();
        //Save(pos, inv);

        stateMashine = new StateMashine();

        stateMashine.SetState(movement);

        movement.OnIdealPositionUpdated += UpdateIdealPosition;
        somersault.OnLayerUpdatedd += LayerUpdate;
        somersault.OnSomersaultUpdate += SomersaultUpdate;
        health.OnChange += DamageUpdate;


        if (SaveSystem.isSaved) LoadPlayer();


        StorableInfo.GenerateAllValuesForDB();
    }

    private void Update()
    {
        
        if (Time.timeScale > 0)
        {
            stateMashine.CurrentState.HandleInput();
            stateMashine.CurrentState.LogicUpdate();

            if (stateMashine.CurrentState != dethState)
            {
                if ((isSomersaulting == false && isAttacking == false) && isDamage)
                {
                    if (isClicedSomersault && isSomersaultColdawn && ((joystick.Horizontal != 0) || (joystick.Vertical != 0)))
                    {
                        StartCoroutine(SomersaultCoroutine());
                    }
                    else if (isClicedAtak && isAtakColdawn)
                    {
                        StartCoroutine(AtakCoroutine());
                    }
                    else if ((joystick.Horizontal != 0) || (joystick.Vertical != 0))
                    {
                        stateMashine.SetState(movement);

                    }
                    else
                    {
                        stateMashine.SetState(ideal);
                    }
                }
            }
        }


    }

    public void IsClikedAtak()
    {
        isClicedAtak = true;
    }

    public void IsClikedSomersault()
    {
        isClicedSomersault = true;
    }

    private IEnumerator SomersaultCoroutine()
    {
        if (stamina.PersCurrentStamina >= 50) 
        {
            stamina.Change(-50);
            isSomersaulting = true;
            isSomersaultColdawn = false;

            stateMashine.SetState(somersault);
          
            yield return new WaitForSeconds(SomersaultCooldownSeconds);
            isClicedSomersault = false;
            isSomersaultColdawn = true;
        }

    }
    private void SomersaultUpdate()
    {
        isSomersaulting = false;
    }



    private IEnumerator AtakCoroutine()
    {
        isAttacking = true;
        isAtakColdawn = false;
        
        stateMashine.SetState(weaponsInteraction);
        yield return new WaitForSeconds(AtakDuration);
        isAttacking = false;
        yield return new WaitForSeconds(AtakCooldownSeconds);
        isClicedAtak = false;
        isAtakColdawn = true;

    }

    private IEnumerator DamageCoroutine()
    {
        isDamage = false;
        stateMashine.SetState(takingDamage);
        yield return new WaitForSeconds(DamageDuration);
        isDamage = true;
        

    }

    private void DamageUpdate(int hp, int damage)
    {
        if (stateMashine.CurrentState != dethState)
        {
            if (hp <= 0)
            {
                stateMashine.SetState(dethState);

            }
            else if (damage < 0)
            {
                StartCoroutine(DamageCoroutine());
            }
        }
    }

    private void FixedUpdate()
    {
        stateMashine.CurrentState.PhysicsUpdate();
    }

    private void LayerUpdate(int layer)
    {
        this.gameObject.layer = layer;
        
    }

    private void UpdateIdealPosition(int position)
    {
        ideal.UpdateIdealPosition(position);
        weaponsInteraction.UpdateIdealPosition(position);
        somersault.UpdateIdealPosition(position);
        takingDamage.UpdateIdealPosition(position);
        dethState.UpdateIdealPosition(position);

    }

    //private void SomersaultUpdate
    public void SavePlayer()
    {
       // SaveSystem.SavePlayer( health.maxHeatpoints,  mana.PersMaxMana,  stamina.PersMaxStamina, transform.position);
    }
    
    public void SaveStats()
    {
       // SaveSystem.SavePlayer(health.maxHeatpoints, mana.PersMaxMana, stamina.PersMaxStamina );
    }
    
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        // Debug.Log(data != null);
       
            Vector3 position;
            //Debug.Log(data.position);
            // Debug.Log(data.heatpoints);
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;

            health.maxHeatpoints = (int)stats.MaxHp;
            health.hitpoints = (int)stats.MaxHp;
            mana.PersMaxMana = (int)stats.MaxMana;
            mana.PersCurrentMana = (int)stats.MaxMana;
            stamina.PersMaxStamina = (int)stats.MaxStamina;
            stamina.PersCurrentStamina = (int)stats.MaxStamina;

            Weapon.damageSkale = stats.Strength;
            Movement.currentMovementSpeed = stats.Speed;
            haste = stats.Haste;
            Intelegent = stats.Intellegence;
            critShance = stats.CritChance;
            critDAmage = stats.CritDamage;
            Lvl = (int)stats.Lvl;

       
       
    }

}
