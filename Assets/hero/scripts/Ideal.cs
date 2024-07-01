using UnityEngine;

public class Ideal : State
{
    private int idealPosition;

    public Ideal(Rigidbody2D rigidbody, Animator animator, Transform transform, CapsuleCollider2D capsuleCollider)
    : base(rigidbody, animator, transform, capsuleCollider)
    {
    }

    public override void LogicUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {

        switch (idealPosition)
        {
            case 1:
                anim.Play("idealRight");
                break;
            case 2:
                anim.Play("idealLeft");
                break;
            case 3:
                anim.Play("idealDown");
                break;
            default:
                anim.Play("ideal");
                break;
        }

    }

    public void UpdateIdealPosition(int position)
    {
        idealPosition = position;
    }
}
