using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

    public static bool playerHit = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHit = true;
        }
    }

}
