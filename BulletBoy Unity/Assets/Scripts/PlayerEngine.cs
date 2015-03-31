using UnityEngine;
using System.Collections;

public class PlayerEngine : MonoBehaviour {

    private float _speed = .1f, _maxSpeed = 60;
    public string walkInput;
    Rigidbody2D rb;

    [Header("Jumping")]
    public float jumpForce;
    public Transform groundCheck;
    private float radius = 0.2f;
    private bool grounded = false;
    public LayerMask whatIsGround;
    private int _jumps = 0, _maxJumps = 2;

    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround); // Check if it is on ground
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rb.velocity.y); // checking vertical speed

        if (grounded) { _jumps = 0; }
            
        Movement();
	}

    /// <summary>
    /// The player moves and jumps around
    /// </summary>
    private void Movement()
    {
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis(walkInput)));
        if (Input.GetAxis(walkInput) < -.1f) // !! Also have to put in the button for controller movement!!
        {
            if (rb.velocity.x > -_maxSpeed)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(-_speed * _maxSpeed, rb.velocity.y);
            }
        }
        else if (Input.GetAxis(walkInput) > .1f)// !! also have to put in the button for controller movement !!
        {
            if (rb.velocity.x < _maxSpeed)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(_speed * _maxSpeed, rb.velocity.y);
            }
        }

        if (Input.GetButtonDown("Jump") && _jumps < _maxJumps)
        {
            Jump();
        }
    }
    /// <summary>
    /// Method for jumping
    /// adds a jumpforce when player jumps and set animation
    /// </summary>
    private void Jump()
    {
        anim.SetBool("Ground", false);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce));
        _jumps++;
    }
}
