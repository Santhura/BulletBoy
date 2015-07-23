using UnityEngine;
using System.Collections;
//using Rewired;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    //private Player player;
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
      //  player = ReInput.players.GetPlayer(0);
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
       /* float Xas = player.GetAxis("Horizontal") * speedX * Time.deltaTime;
        float Zas = player.GetAxis("Vertical") * speedY * Time.deltaTime;*/

        if (/*player.GetAxis("Horizontal") > .4f || player.GetButton("Move Horizontal") ||*/ Input.GetAxis("Horizontal") > .1f)
        {
            rb.velocity = new Vector3(speedX * maxSpeed, rb.velocity.y, rb.velocity.z);
            transform.eulerAngles = new Vector3(0, 180, 20);
        }
        else if (/*player.GetAxis("Horizontal") < -.4f || player.GetNegativeButton("Move Horizontal") ||*/ Input.GetAxis("Horizontal") < -.1f)
        {
            rb.velocity = new Vector3(-speedX * maxSpeed, rb.velocity.y, rb.velocity.z);
            transform.eulerAngles = new Vector3(0, 180, -20);
        }
        else if (/*player.GetAxis("Horizontal") > -.4f && player.GetAxis("Horizontal") < .4f ||*/ Input.GetAxis("Horizontal") > -.1f && Input.GetAxis("Horizontal") < .1f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (/*player.GetAxis("Vertical") > .4f || player.GetButton("Move Vertical") ||*/ Input.GetAxis("Vertical") > .1f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedY * maxSpeed);
            transform.eulerAngles = new Vector3(-10, 180, 0);
        }
        else if (/*player.GetAxis("Vertical") < -.4f || player.GetNegativeButton("Move Vertical") ||*/ Input.GetAxis("Vertical") < -.1f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speedY * maxSpeed);
            transform.eulerAngles = new Vector3(10, 180, 0);
        }
    }

    private void Shooting(float ShotSpeed)
    {
        if (/*player.GetButtonDown("X Button") ||*/ Input.GetButtonDown("Jump"))
        {
           bullets = Instantiate(bulletPrefab, gameObject.transform.FindChild("Cannon").position, new Quaternion(90,0,0,90)) as GameObject;
           bullets.name = "PlayerBullet";
          // Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullets.GetComponent<Collider>());
           Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullets.GetComponent<SphereCollider>());
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
