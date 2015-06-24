using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    private Player player;
    private Rigidbody rb;

    [Header("Movement")]
    public float maxSpeed = 15;
    public float accel = 1.01f;
    public float xas = 4, zas = 2;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 100000;
    private GameObject bullets;

    [Header("Health")]
    public Image healthBar;
    public float hp = 100;

	// Use this for initialization
	void Start () {
        player = ReInput.players.GetPlayer(0);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement(xas, zas);
        Shooting(bulletSpeed);
        HealthPoints();
	}

    private void Movement(float speedX, float speedY)
    {
        float Xas = Input.GetAxis("Horizontal") * speedX * Time.deltaTime;
        float Zas = Input.GetAxis("Vertical") * speedY * Time.deltaTime;

        if (Input.GetAxis("Horizontal") > .9f)
        {
            rb.velocity = new Vector3(Xas, 0, 0);
            transform.eulerAngles = new Vector3(0, 180, 20);
        }
        else if (Input.GetAxis("Horizontal") < -.9f)
        {
            rb.velocity = new Vector3(Xas, 0, 0);
            transform.eulerAngles = new Vector3(0, 180, -20);
        }
        else if (Input.GetAxis("Horizontal") > -.9f && Input.GetAxis("Horizontal") < .9f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetAxis("Vertical") > .9f)
        {
            rb.velocity = new Vector3(0, 0, Zas);
            transform.eulerAngles = new Vector3(-10, 180, 0);
        }
        else if (Input.GetAxis("Vertical") < -.9f)
        {
            rb.velocity = new Vector3(0, 0, Zas);
            transform.eulerAngles = new Vector3(10, 180, 0);
        }

        #region controller movement
        //Todo: fix the input for keyboard and controller

        // Horizontal Movement
        /* if (player.GetButton("Move Horizontal")) 
         {
             if (rb.velocity.x > -maxSpeed)
             {
                 rb.velocity = new Vector3(-accel * speedX, rb.velocity.y, rb.velocity.z);
             }
         }
         else if (player.GetNegativeButton("Move Horizontal"))
         {
             if (rb.velocity.x < maxSpeed)
             {
                 rb.velocity = new Vector3(accel * speedX, rb.velocity.y, rb.velocity.z);
             }
         }
         // vertical movement
         if (player.GetButton("Move Vertical"))
         {
             if (rb.velocity.y > -maxSpeed)
             {
                 rb.velocity = new Vector3(rb.velocity.x, -accel * speedY, rb.velocity.z);
             }
         }
         else if (player.GetNegativeButton("Move Vertical"))
         {
             if (rb.velocity.y < maxSpeed)
             {
                 rb.velocity = new Vector3(rb.velocity.x, accel * speedY, rb.velocity.z);
             }
         }*/
        #endregion
    }

    private void Shooting(float ShotSpeed)
    {
        if (player.GetButtonDown("X Button") || Input.GetKeyDown(KeyCode.Space))
        {
           bullets = Instantiate(bulletPrefab, gameObject.transform.FindChild("Cannon").position, new Quaternion(90,0,0,90)) as GameObject;
           Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullets.GetComponent<Collider>());
           bullets.GetComponent<Rigidbody>().AddForce(Vector3.forward * ShotSpeed * Time.deltaTime);
        }
        Destroy(bullets, 2);
    }

    private void HealthPoints()
    {
        if (hp < 1)
        {
            Destroy(gameObject);
        }
        healthBar.fillAmount = (hp / 100);
    }
}
