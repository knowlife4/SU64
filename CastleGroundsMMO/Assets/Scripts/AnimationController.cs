using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    public Animator anim;

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

    public void Jump() 
    {
        anim.SetTrigger("DefaultJump");
        anim.ResetTrigger("DefaultLand");
    }

    public void Land()
    {
        anim.SetTrigger("DefaultLand");
        anim.ResetTrigger("DefaultJump");
    }

}
