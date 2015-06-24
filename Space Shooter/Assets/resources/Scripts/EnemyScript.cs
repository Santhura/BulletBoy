using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    [Header("Enemy Movement")]
    public float movementSpeed = 2;

    [Header("Attack")]
    public float shotSpeed = 80000, shotTimer = 3, prevTimer;

    private GameObject prefabBullet, bullet;
    public bool canShoot = false;

	// Use this for initialization
	void Start () {
        prefabBullet = Resources.Load("Prefabs/Enemy Bullet", typeof(GameObject)) as GameObject;
        prevTimer = shotTimer;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyBehaviour(movementSpeed);
	}

    void EnemyBehaviour(float speed)
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        Physics.IgnoreLayerCollision(8, 10);

        shotTimer -= Time.deltaTime;
        if (gameObject != null)
        {
            if (shotTimer < 1)
            {
                bullet = Instantiate(prefabBullet, gameObject.transform.position, Quaternion.identity) as GameObject;
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullet.GetComponent<Collider>());
                canShoot = true;
                shotTimer = prevTimer;
            }

            if (canShoot)
                bullet.GetComponent<Rigidbody>().AddForce(-Vector3.forward * shotSpeed * Time.deltaTime);
        }
        else if (gameObject == null)
        {
            Destroy(bullet,2);
        }

        Destroy(bullet, 2);
    }
}
