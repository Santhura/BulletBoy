﻿using UnityEngine;
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
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) { Spawn(); } else { hasSpawned = false; }

        if (Spawn())
        {
            EnemyBehavior();
            FlyingAttack();
            if (enemyType == "Flying")
            {
                if (dropCoolDown > 0)
                {
                    dropCoolDown -= Time.deltaTime;
                }
            }
        }

	}

    void EnemyBehavior()
    {
        if (walkingRight)
        {
            anim.SetTrigger("Walk");
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            if (enemyType == "Flying")
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (enemyType == "Bomb")
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            anim.SetTrigger("Walk");
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (enemyType == "Flying")
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (enemyType == "Bomb")
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    bool Spawn()
    {
       return hasSpawned = true;
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
                bombDrop.transform.position = new Vector3(gameObject.transform.FindChild("DropPlace").position.x,gameObject.transform.FindChild("DropPlace").position.y, 1);
            }
            else
            {
                anim.SetBool("Attack", false);
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
}