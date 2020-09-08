using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    //Public Variables
        //public transforms
        public Transform GameModel;
        public AnimationController animControl;

        //public floats
        public float Speed;
        public float LerpTime;
        public float GroundedRay;
        public float MaxFloorSlope;
        public float JumpForce;
        public float smooth = 5f;

        //public sounds
        public AudioClip[] JumpSounds;

        //public bools
        public bool Grounded;
        public bool CanJump = true;
        public bool CanWalk = true;


    //Private Variables
    Vector3 InputRaw;
    Vector3 relativeMovement;
    Transform t_Reference;
    Rigidbody body;
    RigidbodyConstraints previousConstraints;
    RaycastHit hit;
    CapsuleCollider capsule;
    float LowestY;
    RaycastHit LowestHit;
    AudioSource source;
    
    bool Jumping;
    public int JumpCount;

    void Start()
    {
        source = GetComponent<AudioSource>();
        t_Reference = new GameObject().transform;
		t_Reference.transform.parent = transform;
        body = GetComponent<Rigidbody>();
        previousConstraints = body.constraints;
        capsule = GetComponent<CapsuleCollider>();
        LowestHit = new RaycastHit();
    }

    void LateUpdate()
    {
        t_Reference.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        body.velocity = new Vector3(0,body.velocity.y,0);
        if (Input.GetButtonDown("Jump") && CanJump && JumpCount < 1) {
			Jumping = true;
			body.velocity = new Vector3(body.velocity.x, JumpForce, body.velocity.z);
			JumpCount++;
            animControl.Jump();
            source.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Length)], .5f);
		}
    }

    void FixedUpdate()
    {
        RaycastHit hit1;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit1, 2f))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(GameModel.up, hit1.normal) * GameModel.rotation;
            GameModel.rotation = Quaternion.RotateTowards(GameModel.rotation, targetRotation , smooth * Time.deltaTime);
        }
        FixedMovement();
        Ground();
    }

    //Movement Processing
    void FixedMovement ()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && CanWalk){

            transform.Translate(new Vector3(relativeMovement.x, 0, relativeMovement.z) * (Speed));
			//body.constraints = previousConstraints;
            GameModel.forward = Vector3.Lerp(GameModel.forward, new Vector3(relativeMovement.x, 0, relativeMovement.z),
			LerpTime);
        }
        else {
            //body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }

        InputRaw = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);

        float TurnAngle = Mathf.Abs(Vector3.Angle(new Vector3(GameModel.forward.x, 0, GameModel.forward.z),
			new Vector3(relativeMovement.x, 0, relativeMovement.z)));

        relativeMovement = t_Reference.TransformVector(InputRaw);
    }

    void Ground () {
        LowestY = float.MaxValue;
        GroundRaycast(capsule.transform.position);
		GroundRaycast(new Vector3(capsule.transform.position.x, capsule.transform.position.y,
			capsule.transform.position.z + capsule.radius));
		GroundRaycast(new Vector3(capsule.transform.position.x + capsule.radius, capsule.transform.position.y,
			capsule.transform.position.z));
		GroundRaycast(new Vector3(capsule.transform.position.x - capsule.radius, capsule.transform.position.y,
			capsule.transform.position.z));
		GroundRaycast(new Vector3(capsule.transform.position.x, capsule.transform.position.y,
			capsule.transform.position.z - capsule.radius));
       if ((LowestY == float.MaxValue || (Jumping && body.velocity.y <= 0))) {
			if (Jumping) Jumping = false;
			if (Grounded) Grounded = false;
		}
		else {
			float SlopeAngle = Mathf.Abs(Vector3.Angle(Vector3.up, LowestHit.normal));
			if ((MaxFloorSlope == 0 || SlopeAngle < -MaxFloorSlope || SlopeAngle > MaxFloorSlope)) {
				if (Grounded) {
					Grounded = false;
				}
			}
			else if (!Grounded && !Jumping) {
				OnLand();
			}
        
        }
    }

    void OnLand () {
        Grounded = true;
        JumpCount = 0;
        animControl.Land();
    }

    public void ReceiveKnockback(Vector3 knockVector) {
		body.velocity = knockVector;
	}
    
    private void GroundRaycast(Vector3 origin) {
		if (Physics.Raycast(origin, -Vector3.up, out hit, capsule.height / 2f * 1.2f,
			~LayerMask.GetMask("Player")))
			if (hit.point.y < LowestY) {
				LowestY = hit.point.y;
				LowestHit = hit;
			}
	}
}
