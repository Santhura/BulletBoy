using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour {

    [Header("Speed")]
    public float walkSpeed;

    private float jumpForce = 400;

    private Rigidbody2D rb;

    [Header("Is on ground")]
    public LayerMask whatIsGround; //all things that is ground
    public GameObject ground; 

    private bool _grounded = false;
    private const float _radius = 0.1f; //  range for check if it is on ground
    
    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
	
	void Start () {
	
	}
	

	void FixedUpdate () {
        _grounded = Physics2D.OverlapCircle(ground.transform.position, _radius, whatIsGround);
        Movement();
	}

    void Movement()
    {
        Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), 0) * walkSpeed; //use mobile joystick to move
        transform.Translate(Vector2.right * moveVec.x);
        
        // only when you are on the ground you can select something or jump
        if (_grounded)
        {
            if (CrossPlatformInputManager.GetButton("A_Button"))
            {
                // todo:: if in range from npc the selection else jumping
                rb.AddForce(new Vector2(0, jumpForce));
            }
            else if (CrossPlatformInputManager.GetButton("B_Button"))
            {
                //todo:: cancel stuff
            }
        }
        
    }
}
