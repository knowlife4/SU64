using UnityEngine;
using System.Collections;
using UnityEngine.Networking; 

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraScript : MonoBehaviour {
 
    public Transform target;
    public float distance = 5.0f;
	public float basicdistance = 5.0f;
	public float correctspeed;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
	public bool isTriggered;
	public float checkDist;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
	public GameObject playerRender;
    public float distanceMin = .5f;
    public float distanceMax = 15f;
 
    private Rigidbody rigidbody;
 
    float x = 0.0f;
    float y = 0.0f;
 
    // Use this for initialization
    void Start () 
    {
		target = transform.parent;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        rigidbody = GetComponent<Rigidbody>();
 
        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

	    //_overlay = target.GetComponent<PlayerOverlay>();
        //GetComponent<PostProcessingBehaviour>().profile = LevelProperties.PostProcessing;
    }
 
    void LateUpdate () 
    {
        if(Input.GetKeyDown("t")){Cursor.lockState = CursorLockMode.Locked;}
        if (target/* && !_overlay.MenuIsUp*/) 
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 
            y = ClampAngle(y, yMinLimit, yMaxLimit);
 
            Quaternion rotation = Quaternion.Euler(y, x, 0);
 
			basicdistance = Mathf.Clamp(basicdistance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
            RaycastHit hit;
            if (Physics.Linecast (target.position, transform.position, out hit) && isTriggered == false) 
            {
                distance -=  correctspeed;
            }
			else{
				if(distance < basicdistance && isTriggered == false){distance += correctspeed;}
			}
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
 
            transform.rotation = rotation;
            transform.position = position;
			if(Physics.Raycast(transform.position, -transform.forward, checkDist)){
				isTriggered = true;
			}
			else{isTriggered = false;}
	
    }}
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}