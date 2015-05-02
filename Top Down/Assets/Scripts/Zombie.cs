using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Zombie : MonoBehaviour {

    private float health = 1, radius = 3;
    public LayerMask playerLayer;
    private GameObject player;
    private bool spotted = false;

    public Image healthBar;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + .5f, healthBar.transform.position.z);
        healthBar.fillAmount = health;
        Flocking(transform.position, radius);
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
}