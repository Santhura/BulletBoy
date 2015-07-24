using UnityEngine;
using System.Collections;

public class BombDropScript : MonoBehaviour {

    float fallingSpeed = 3;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 1.6f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Physics2D.IgnoreLayerCollision(14,13);
	}
    void OnCollisionEnter2D(Collision2D col)
    {
     if(col.transform.tag != "Enemy")
        anim.SetTrigger("Explode");
        
        if (col.transform.tag == "Player")
        {
            // do something
        }
    }
}
