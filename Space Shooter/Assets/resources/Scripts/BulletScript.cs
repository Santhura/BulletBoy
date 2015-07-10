using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    float enemyShotSpeed = 1000, playerShotSpeed = 100000;
	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(8, 9);
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.name == "PlayerBullet")
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * playerShotSpeed * Time.deltaTime);
        }
        else if (gameObject.name == "EnemyBullet")
        {
            GetComponent<Rigidbody>().AddForce(-Vector3.forward * enemyShotSpeed * Time.deltaTime);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (gameObject.name != "EnemyBullet")
            {
                Destroy(gameObject);
                Destroy(col.gameObject);
                EnemySpawn.spawnCounter--;
            }
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            col.gameObject.GetComponent<PlayerScript>().hp -= Random.Range(10, 21);
        }
    }
}
