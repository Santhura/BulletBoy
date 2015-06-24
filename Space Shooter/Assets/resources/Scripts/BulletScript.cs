using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    PlayerScript player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        Physics.IgnoreLayerCollision(8, 9);
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
            EnemySpawn.spawnCounter--;
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.hp -= Random.Range(10, 21);
        }
    }
}
