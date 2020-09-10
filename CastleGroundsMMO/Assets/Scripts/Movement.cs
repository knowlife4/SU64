using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public AnimationController animControl;

    public float speed = 5; // units per second
    public float turnSpeed = 90f; // degrees per second
    public float jumpSpeed = 8f;
    public float gravity = 9.8f;
    public float LerpTime;
    public float MaxFloorSlope;
    public float GroundedRay;

    public Transform GameModel;

    public bool Grounded;
    public bool ccGrounded;
    public bool CanJump = true;
    public bool CanWalk = true;


    public AudioClip[] JumpSounds;
    public AudioClip[] DoubleJumpSounds;
    public AudioClip[] TripleJumpSounds;
    

    float vSpeed = 0f;
    CharacterController controller;
    Vector3 InputRaw;
    Vector3 movingDirection = Vector3.zero;
    Vector3 relativeMovement;
    Transform t_Reference;
    RaycastHit hit;
    CapsuleCollider capsule;
    float LowestY;
    RaycastHit LowestHit;
    AudioSource source;

    public bool Jumping;
    public int JumpCount;
    public bool moving = false;

    void Awake () 
    {
        source = GetComponent<AudioSource>();
        capsule = GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
        t_Reference = new GameObject().transform;
		t_Reference.transform.parent = transform;
        OnLand();
    }

    void LateUpdate () 
    {
        JumpUpdate();
    }


    void Update () 
    {
        t_Reference.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        MoveUpdate();

        if(controller.isGrounded && Jumping)
        {
            OnLand();
        }
    }

    public void JumpCountReset () 
    {
        JumpCount = 0;
    }

    void MoveUpdate () 
    {
        Vector3 vel = new Vector3(GameModel.forward.x, vSpeed, GameModel.forward.z) * relativeMovement.magnitude * (speed);
        InputRaw = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);
        relativeMovement = t_Reference.TransformVector(InputRaw);

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            moving = true;
            GameModel.forward = Vector3.Lerp(GameModel.forward, new Vector3(relativeMovement.x, 0, relativeMovement.z), LerpTime);
            if(animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("WalkBlend"))
            {
                JumpCount = 0;
            }
        }
        else
        {
            if(moving)
            {
                moving = false;
            }
            if(JumpCount >= 2)
            {
                JumpCount = 0;
            }
        }
        if(!controller.isGrounded)
        {
            vSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            if(!Jumping)
            {
                vSpeed = transform.position.y-10;
            }
        }
        vel.y = vSpeed;
        controller.Move(vel * Time.deltaTime);
    }

    void Jump(float force, AudioClip sound, AnimationController.JumpType type)
    {
        animControl.Jump(type);
        vSpeed = force;
        Jumping = true;
        source.PlayOneShot(sound, .5f);
    }

    void JumpUpdate () 
    {
        if (Input.GetButtonDown("Jump") && CanJump && controller.isGrounded) {
            if(JumpCount == 2)
            {
                if(animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("DoubleJumpLand"))
                {
                    Jump(35f, TripleJumpSounds[Random.Range(0, TripleJumpSounds.Length)], AnimationController.JumpType.Triple);
                    JumpCount++;  
                }
                
            }
            if(JumpCount == 1)
            {
                if(animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("DefaultJumpLand"))
                {
                    Jump(26f, DoubleJumpSounds[Random.Range(0, DoubleJumpSounds.Length)], AnimationController.JumpType.Double);
                    JumpCount++;
                }
                else
                {
                   JumpCount = 0;
                }
            }
            if(JumpCount == 0)
            {
                Jump(21f, JumpSounds[Random.Range(0, JumpSounds.Length)], AnimationController.JumpType.Default);
                JumpCount++;
            }
		}
    }

    void OnLand () {
        Grounded = true;
        animControl.Land();
        if(Jumping)
        {
            Jumping = false;
        }
    }
}
