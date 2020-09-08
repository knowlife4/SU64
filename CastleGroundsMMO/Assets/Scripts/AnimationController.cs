using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    public Animator anim;
    public enum JumpType {Default, Double, Triple};

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            anim.SetFloat("Blend", new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical")).magnitude);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    public void Skid () 
    {
        anim.SetTrigger("Skid");
    }

    public void Jump(JumpType type) 
    {
        if(type == JumpType.Default)
        {
            anim.SetTrigger("DefaultJump");
            anim.ResetTrigger("DefaultLand");
        }
        if(type == JumpType.Double)
        {
            anim.SetTrigger("DoubleJump");
            anim.ResetTrigger("DoubleLand");
        }
        if(type == JumpType.Triple)
        {
            anim.SetTrigger("TripleJump");
            anim.ResetTrigger("TripleLand");
        }
    }

    public void Land()
    {
        anim.SetTrigger("DefaultLand");
        anim.ResetTrigger("DefaultJump");
        anim.SetTrigger("DoubleLand");
        anim.ResetTrigger("DoubleJump");
        anim.SetTrigger("TripleLand");
        anim.ResetTrigger("TripleJump");
        grounded(true);
    }

    public void grounded (bool isGrounded) 
    {
        if(isGrounded)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }
    }

}
