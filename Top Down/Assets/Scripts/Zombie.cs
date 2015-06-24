using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Enum that keeps track of what the Zombie is currently doing
public enum MovementState : int
{
    IDLE,
    MOVING,
    ORGANIZING
}

public class Zombie : MonoBehaviour {

    private float health = 1, radius = 3;
    public LayerMask playerLayer;
    private GameObject player;
    private bool spotted = false;

    
    private Rigidbody2D rigid;
    public Image healthBar;
    
    //Property for the current movement state
    private MovementState currentMovementState;
    public MovementState CurrentMovementState
    {
        get 
        {
            return currentMovementState; 
        }
        set
        {
            currentMovementState = value;
        }
    }
    //The intensity in which Zombie will be attracted to each other.
    public float coherencyWeight = 0;
    //The intensity in which Zombie will be repelled from each other.
    public float separationWeight = 1;
    // The Calculated vector of oth the coherency and separation behaviors.
    private Vector3 zombieBehaviorForce;
    private Vector3 relativePos, coherency, separation;

    List<Zombie> zombies;

    public int pingsPerSecond = 10;
    public float PingFrequency
    {
        get { return (1 / pingsPerSecond); }
    }

    public LayerMask radarLayers;

    private List<Zombie> neighbors = new List<Zombie>();
    private Collider[] detected;

    //It finds all the Zombies in the scene and adds them to a list calls 'zombies'
    // and sets the  current movement state to IDLE.
    // Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();

        Zombie[] foundZombies = FindObjectsOfType(typeof(Zombie)) as Zombie[];
        zombies = new List<Zombie>();
        foreach (Zombie zombie in foundZombies)
        {
            zombies.Add(zombie);
        }
        CurrentMovementState = MovementState.IDLE;
        StartCoroutine("StartTick", PingFrequency);
        
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + .5f, healthBar.transform.position.z);
        healthBar.fillAmount = health;
      //  Flocking(transform.position, radius);
        ZombieBehaviors();
	}

    public Vector3 ZombieBehaviors()
    {
        coherency = Vector3.zero - transform.position; 
        
        separation = Vector3.zero;

        foreach (Zombie z in neighbors)
        {
            if (z != this)
            {
                relativePos = (transform.position - z.transform.position);
                separation += relativePos / relativePos.sqrMagnitude;
            }
        }

        zombieBehaviorForce = (coherency * coherencyWeight) + (separation * separationWeight);
        zombieBehaviorForce.y = 0;

        return zombieBehaviorForce;
    }


    void Flocking(Vector3 center, float radius) 
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, playerLayer);
        if (hitColliders.Length >= 1)
        {
            spotted = true;
        }

        if (spotted)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, .01f);
        }
    }

    public void DoDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(healthBar);
        }
    }

    private IEnumerable StartTick(float freq)
    {
        Debug.Log("come on");
        yield return new WaitForSeconds(freq);
        RadarScan();
    }

    private void RadarScan()
    {
        neighbors.Clear();
        detected = Physics.OverlapSphere(transform.position, radius, radarLayers);

        foreach (Collider c in detected)
        {
            if (c.GetComponent<Zombie>() != null && c.gameObject != this.gameObject)
            {
                Zombie foundZombie = c.GetComponent<Zombie>() as Zombie;
                neighbors.Add(foundZombie);
            }
        }

        if(neighbors.Count == 0 &&  currentMovementState != MovementState.MOVING && CurrentMovementState == MovementState.ORGANIZING) {
            Debug.LogWarning(currentMovementState);
            CurrentMovementState = MovementState.IDLE;
        }
        StartCoroutine("StartTick", PingFrequency);
    }
}