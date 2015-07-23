using UnityEngine;
using System.Collections;

public abstract class EnemyAI : MonoBehaviour {

    [Header("Type")]
    public string enemyType;

    private bool hasSpawned = false, walkingRight = true;

    [Header("Collision Check")]
    public Transform objectCheck;
    private bool hit;
    public float radius = .1f;
    public LayerMask whatIsGround;

    [Header("Walking")]
    public float speed = 3;

    Animator anim;
    Rigidbody2D rb;

    public virtual void Awake()
    {

    }

	// Use this for initialization
	public virtual void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        hit = Physics2D.OverlapCircle(objectCheck.position, radius, whatIsGround);

        if (hit) { walkingRight = !walkingRight; }
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) { Spawn(); } else { hasSpawned = false; }

        if (hasSpawned)
        {
            EnemyBehavior();
        }

	}

    void EnemyBehavior()
    {
        if (walkingRight)
        {
            anim.SetTrigger("Walk");
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            anim.SetTrigger("Walk");
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Spawn()
    {
        hasSpawned = true;
    }
}
