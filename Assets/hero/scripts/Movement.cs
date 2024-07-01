using UnityEngine;

public class Movement : State
{
    public delegate void IdealPositionUpdated(int position);
    public event IdealPositionUpdated OnIdealPositionUpdated;
    private const float NormalMovementSpeed = 1.5f;
    public static float currentMovementSpeed = NormalMovementSpeed;
    private Vector2 moveInput;
    private int idealPosition;
    private Joystick joystick;

    private bool isMoving;
    private Vector2 lastInput;

    public Movement(Rigidbody2D rigidbody, Animator animator, Transform transform, CapsuleCollider2D capsuleCollider, Joystick joystick)
        : base(rigidbody, animator, transform, capsuleCollider)
    {
        this.joystick = joystick;
    }

    public override void HandleInput()
    {
        GetInput();
    }

    public override void LogicUpdate()
    {
        UpdateAnimation();
    }

    public override void PhysicsUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;

        if (isMoving)
        {
            if ((input.x != -lastInput.x && input.x != 0) || (input.y != -lastInput.y && input.y != 0))
            {
                moveInput = input;
                lastInput = input;
              
            }
            isMoving = false;
        }
        else
        {
            if (input.x != 0 || input.y != 0)
            {
                isMoving = true;
                moveInput = input;
                lastInput = input;
               
            }
        }
    }

    private void Move()
    {
       
        if (moveInput != Vector2.zero)
        {
            transform.Translate(moveInput * (currentMovementSpeed * Time.fixedDeltaTime));
        }
       
    }

    private void UpdateAnimation()
    {
             //Debug.Log(joystick.Direction);
            //Debug.Log(joystick.);

             if ((joystick.Horizontal == 1)&& ((joystick.Vertical <= 0.5f) && (joystick.Vertical >= -0.5f)))
            {
                anim.Play("moveRight");
                idealPosition = 1;
            }
            if ((joystick.Horizontal == -1) && ((joystick.Vertical <= 0.5f) && (joystick.Vertical >= -0.5f)))
            {
                anim.Play("moveLeft");
                idealPosition = 2;
            }
            if ((joystick.Vertical == 1) && ((joystick.Horizontal <= 0.5f)&&(joystick.Horizontal >= -0.5f)))
            {
                anim.Play("moveDown");
                idealPosition = 3;
            }
            else if (((joystick.Horizontal == 1) || (joystick.Horizontal == -1)) && (joystick.Vertical == 1))
            {
                anim.Play("moveDown");
                idealPosition = 3;
            }
            if ((joystick.Vertical == -1) && ((joystick.Horizontal <= 0.5f) && (joystick.Horizontal >= -0.5f)))
            {
                idealPosition = 4;
                anim.Play("moveUp");
            }
            else if (((joystick.Horizontal == 1) || (joystick.Horizontal == -1)) && (joystick.Vertical == -1))
            {
                idealPosition = 4;
                anim.Play("moveUp");

            }
            OnIdealPositionUpdated?.Invoke(idealPosition);
    }
}

