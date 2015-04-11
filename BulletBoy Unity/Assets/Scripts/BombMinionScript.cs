using UnityEngine;
using System.Collections;

public class BombMinionScript : MonoBehaviour {

    public float _speed = 3;
    Rigidbody2D rb;
    private bool walkingRight = true, hasSpawned = false;

    public Transform objectCheck;
    private bool hit;
    private float radius = .01f;
    public LayerMask whatIsHit;

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        hit = Physics2D.OverlapCircle(objectCheck.position, radius, whatIsHit); // checks if enemy hits a object
        Physics2D.IgnoreLayerCollision(9, 9);

        if (hit) { walkingRight = !walkingRight; } // if enemy hits object turn around

        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) { Spawn(); } else { hasSpawned = false; }
        if (hasSpawned)
        {
            EnemyBehaviour();
        }
	}

    /// <summary>
    /// Minion Movement behaviour walk always to the right until it hits a object
    /// </summary>
    void EnemyBehaviour()
    {
        if (walkingRight)
        {
            anim.SetTrigger("GoWalk");
            transform.Translate(-Vector2.right * _speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            anim.SetTrigger("GoWalk");
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Spawn() 
    {
        hasSpawned = true;
    }
}
