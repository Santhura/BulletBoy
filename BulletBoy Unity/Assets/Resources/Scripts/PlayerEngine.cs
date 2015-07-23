using UnityEngine;
using System.Collections;

public class PlayerEngine : MonoBehaviour
{

    private float _speed = 10f; // walking speed
    public string walkInput; // Horizontal
    Rigidbody2D rb;

    [Header("Jumping")]
    public float jumpForce;
    public Transform groundCheck;
    private float radius = 0.2f;
    public bool grounded = false;
    public LayerMask whatIsGround; // all layers of what is ground 
    private int _jumps = 0, _maxJumps = 2; // maximum of jumps

    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis(walkInput))); // when there is input play anitmation
        if (Input.GetAxis(walkInput) < -.1f) // !! Also have to put in the button for controller movement!!
        {
            transform.localScale = new Vector3(-1, 1, 1);// change the sprite to the right direction
            transform.Translate(-Vector2.right * _speed * Time.deltaTime); // move the player to the left
        }
        else if (Input.GetAxis(walkInput) > .1f)// !! also have to put in the button for controller movement !!
        {
            transform.localScale = new Vector3(1, 1, 1); // change the sprite to the right direction
            transform.Translate(Vector2.right * _speed * Time.deltaTime); // move the player to the right
        }
        if (Input.GetButtonDown("Jump") && _jumps < _maxJumps) // when press on the jump button activate Jump method
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
