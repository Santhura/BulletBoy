using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    [Header("Enemy Movement")]
    public float movementSpeed = 2;

    [Header("Attack")]
    public float shotSpeed = 80000;

    private GameObject prefabBullet, bullet;
    public bool canShoot;

	// Use this for initialization
	void Start () {
        prefabBullet = Resources.Load("Prefabs/Enemy Bullet", typeof(GameObject)) as GameObject;
        canShoot = true;
    }
	
	// Update is called once per frame
	void Update () {
        EnemyBehaviour(movementSpeed);
        if (canShoot)
        {
            Shooting();
            StartCoroutine(WaitForNextShot(2));
        }
	}

    void EnemyBehaviour(float speed)
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        Physics.IgnoreLayerCollision(8, 10);

    }

    void Shooting()
    {
        if (gameObject != null)
        {
            bullet = Instantiate(prefabBullet, gameObject.transform.FindChild("Cannon").position, new Quaternion(270, 0, 0, -270)) as GameObject;
            bullet.name = "EnemyBullet";
            //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullet.GetComponent<Collider>());
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bullet.GetComponent<SphereCollider>());
        }
        else if (gameObject == null)
        {
            Destroy(bullet);
        }
        canShoot = false;
        Destroy(bullet, 2);
    }
    IEnumerator WaitForNextShot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
}
