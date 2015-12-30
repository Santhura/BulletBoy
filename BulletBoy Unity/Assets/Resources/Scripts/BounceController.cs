using UnityEngine;
using System.Collections;

public class BounceController : MonoBehaviour {

    Rigidbody2D playerRB;
    public float bounceForce = 10000;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// when player hits bounce object set standaard force to bounce
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("hit");
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce( new Vector2(0, bounceForce));
        }
    }
}
