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

    [Header("Flying Enemy Variable")]
    public float dropRate;
    private float dropCoolDown;

    [Header("Cannon enemy variables")]
    public float cannonForceLeft;
    public float cannonForceUp;

    Animator anim;
    Rigidbody2D rb;

    public virtual void Awake()
    {

    }

	// Use this for initialization
	public virtual void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dropCoolDown = 0.0f;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        hit = Physics2D.OverlapCircle(objectCheck.position, radius, whatIsGround);

        if (hit) { walkingRight = !walkingRight; }
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) { hasSpawned = true; }/* else { hasSpawned = false; }*/

        if (hasSpawned)
        {
            EnemyBehavior();
            FlyingAttack();
            CannonEnemy(cannonForceLeft, cannonForceUp);
            if (enemyType == "Flying" || enemyType == "Cannon")
            {
                if (dropCoolDown > 0)
                {
                    dropCoolDown -= Time.deltaTime;
                }
            }
        }

        if (transform.position.y < GameObject.Find("Death Point").transform.position.y)
            Destroy(gameObject);
	}

    void EnemyBehavior()
    {
        if (walkingRight)
        {
            anim.SetTrigger("Walk");
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            if (enemyType == "Spike")
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
            else if (enemyType == "Bomb")
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(enemyType == "Flying")
                transform.localScale = new Vector3(1.5f, 1.5f, 1);

        }
        else
        {
            anim.SetTrigger("Walk");
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (enemyType == "Spike")
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            else if (enemyType == "Bomb")
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (enemyType == "Flying")
                transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }
    }


    /// <summary>
    /// A method for the flying bomb enemy for attacking
    /// </summary>
    void FlyingAttack()
    {
        if (enemyType == "Flying")
        {
            if (CanDrop)
            {
                anim.SetBool("Attack", true);
                dropCoolDown = dropRate;

                GameObject bombDrop = Instantiate(Resources.Load("Prefabs/Enemies/Bomb", typeof(GameObject))) as GameObject;
                bombDrop.transform.position = new Vector3(gameObject.transform.FindChild("DropPlace").position.x, gameObject.transform.FindChild("DropPlace").position.y, 1);
            }
            else
            {
                //anim.SetBool("Attack", false);
            }
        }
    }

    /// <summary>
    /// returns the time when a bomb can be dropped
    /// </summary>
    public bool CanDrop
    {
        get { return dropCoolDown <= 0.0f; }
    }

    /// <summary>
    /// Cannon enemy shoots bomber enemies in a arc
    /// </summary>
    /// <param name="fireForceLeft"></param>
    /// <param name="fireForceUp"></param>
    void CannonEnemy(float fireForceLeft, float fireForceUp)
    {
        if (enemyType == "Cannon")
        {
            if (CanDrop)
            {
                // todo: cannon animation
                anim.SetTrigger("canShoot");
                dropCoolDown = dropRate;
                GameObject bomber = Instantiate(Resources.Load("Prefabs/Enemies/Bomb_Minion", typeof(GameObject))) as GameObject;
                bomber.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                bomber.GetComponent<Rigidbody2D>().AddForce(Vector2.left * fireForceLeft);
                bomber.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fireForceUp);

            }
        }
    }
}
