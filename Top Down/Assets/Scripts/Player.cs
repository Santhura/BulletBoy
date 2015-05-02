using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float speed = 3, angle, bulletSpeed = 10000;
    public string inputV, inputH; //vertical/ horizontal input
    public GameObject bulletPrefab;
    GameObject bullet;
    private Vector2 mousePos, lookPos;
    public Text UI_Ammo;
    private int ammo = 12, maxAmmo = 60,
                oldAmmo;
   
    // Use this for initialization
    void Start()
    {
        oldAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot(bulletSpeed);
        UI_Ammo.text = "Ammo: " + ammo + "/" + maxAmmo;
        Reload();

        if (ammo < 1)
            ammo = 0;
    }

    /// <summary>
    /// The movement of the player
    /// </summary>
    void Movement()
    {
        // turn towards the mouse position
        mousePos = Input.mousePosition;
        lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos.x -= transform.position.x;
        lookPos.y -= transform.position.y;
        angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float Yas = Input.GetAxis(inputV) * speed * Time.deltaTime;
        transform.Translate(Yas, 0, 0);
    }

    /// <summary>
    /// create a bullet and addforce to it. can only shoot bullet if you have more dan 0 bullets
    /// </summary>
    /// <param name="bulletSpeed"></param>
    void Shoot(float bulletSpeed)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                bullet = Instantiate(bulletPrefab, new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, 1), Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody2D>().AddForce(lookPos * bulletSpeed * Time.deltaTime);
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
                ammo--;
            }
        }
    }

    /// <summary>
    /// Check if ammo is less then 0
    /// Check if you can reload
    /// do reload
    /// </summary>
    void Reload() {
        if (maxAmmo > 60)
            maxAmmo = 60;

        if (ammo < 12 && maxAmmo > 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                int newMaxAmmo = oldAmmo - ammo;
                maxAmmo -= newMaxAmmo;
                ammo = 12;
            }
        }

        if (maxAmmo < 1)
            maxAmmo = 0;
    }
}