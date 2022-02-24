using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    float turnRight = 1f;
    float turnLeft = -1f;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    public void WalkRight()
    {
        Walk(turnRight);
    }
    public void WalkLeft()
    {
        Walk(turnLeft);
    }
    void Walk(float direction)
    {
        gameObject.transform.localScale = new Vector3(direction, 1, 1);
        myAnimator.SetBool("isRunning", true);
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * walkingSpeed;
    }
    public void StopWalking()
    {
        myAnimator.SetBool("isRunning", false);
    }

    public void JumpUp()
    {
        if (CheckIfGrounded())
        {
            myAnimator.SetTrigger("jumpUp");
            myRigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    public void JumpAnimation()
    {
        myAnimator.SetFloat("yVelocity", myRigidbody2D.velocity.y);
    }
    public bool CheckIfGrounded()
    {
        if (Mathf.Abs(myRigidbody2D.velocity.y) < 0.001f)
        {
            myAnimator.SetBool("isGrounded", true);
            return true;
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
            return false;
        }
    }

}
