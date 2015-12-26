using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour {

    [Header("Speed")]
    public float walkSpeed;


    private Rigidbody2D rb;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	void Start () {
	
	}
	

	void FixedUpdate () {

        Movement();
	}

    void Movement()
    {
        Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * walkSpeed; //use mobile joystick to move
        transform.Translate(new Vector3(moveVec.x, moveVec.y, 0));
        
    }
}
