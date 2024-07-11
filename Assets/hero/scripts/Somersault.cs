
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Somersault : State
{
    public delegate void LayerUpdated(int layer);
    public Action OnSomersaultUpdate;
    public event LayerUpdated OnLayerUpdatedd;
    private const float SomersaultSpeedMultiplier = 3.0f;

    private const float SomersaultDuration = 0.66f;
    private float somersaultTimer;

    private const float NormalMovementSpeed = 1.5f;
    private float currentMovementSpeed = NormalMovementSpeed;
    private Joystick joystick;

    private int idealPosition;
    
    private Vector2 moveInput;
    private bool isPlayingSomersaultAnimation = false;
    private float somersaultStartTime;

    private bool isSomersaulting = false;
    private Vector2 somersaultDirection;

    public Somersault(Rigidbody2D rigidbody, Animator animator, Transform transform, CapsuleCollider2D capsuleCollider, Joystick joystick)
        : base(rigidbody, animator, transform, capsuleCollider)
    {
        this.joystick = joystick;
        
    }
    public override void Enter()
    {
        base.Enter();
        OnLayerUpdatedd?.Invoke(7);
        somersaultTimer = 0;
    }

    public override void Exit()
    {
        base.Exit();
        OnLayerUpdatedd?.Invoke(6);
        somersaultTimer = 0;
    }


    public override void HandleInput()
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isSomersaulting)
        {
            GetInput(); 
        }


        if (!isPlayingSomersaultAnimation)
        {
            PlaySomersaultAnimation();
        }
        else
        {
            UpdateSomersaultAnimation();
        }
    }

    public override void PhysicsUpdate()
    {
        somersaultTimer += Time.deltaTime;
        if (somersaultTimer >= SomersaultDuration) OnSomersaultUpdate?.Invoke();
        Move();
    }

    private void PlaySomersaultAnimation()
    {
        somersaultDirection = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        currentMovementSpeed = NormalMovementSpeed * SomersaultSpeedMultiplier;
       
        if (((joystick.Horizontal == 1) || (idealPosition == 1)) && (joystick.Vertical == 0)) { anim.Play("SomRight"); }
        if (((joystick.Horizontal == -1) || (idealPosition == 2)) && (joystick.Vertical == 0)) anim.Play("SomLeft");
        if (((joystick.Vertical == 1) || (idealPosition == 3)) && (joystick.Horizontal == 0)) anim.Play("SomDown");
        else if (((joystick.Horizontal == 1) || (joystick.Horizontal == -1)) && (joystick.Vertical == 1)) anim.Play("SomDown");
        if (((joystick.Vertical      == -1) || (idealPosition == 4)) && (joystick.Horizontal == 0)) anim.Play("SomUp");
        else if (((joystick.Horizontal == 1) || (joystick.Horizontal == -1)) && (joystick.Vertical == -1)) anim.Play("SomUp");

        somersaultStartTime = Time.time;
        isPlayingSomersaultAnimation = true;
        isSomersaulting = true;
    }

    private void UpdateSomersaultAnimation()
    {
        if (Time.time > somersaultStartTime + SomersaultDuration)
        {
            currentMovementSpeed = NormalMovementSpeed;
            isPlayingSomersaultAnimation = false;
            isSomersaulting = false; 
        }
    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }

    private void Move()
    {
        

            if (isSomersaulting)
            {
                // Используем сохраненное направление для движения во время переката
                transform.Translate(somersaultDirection * (currentMovementSpeed * Time.fixedDeltaTime));
            }
            else
            {
                transform.Translate(moveInput * (currentMovementSpeed * Time.fixedDeltaTime));
            }
        
    }

    private void GetInput()
    {
        moveInput.x = joystick.Horizontal;
        moveInput.y = joystick.Vertical;
    }
}