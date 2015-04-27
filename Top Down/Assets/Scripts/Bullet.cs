using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private float damage = Random.Range((float).1f,(float).3f);
    public GameObject particleHit;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Zombie zombie = other.gameObject.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.DoDamage(damage);
            Instantiate(particleHit, new Vector3(transform.position.x,transform.position.y, -1), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
