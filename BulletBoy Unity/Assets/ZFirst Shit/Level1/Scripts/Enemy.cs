using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isOnGround = false, 
				hitWallL = false,
				walkingLeft,
				walkingRight,
				hasSpawn,
				isDead = false;

	public Transform groundEnd, 
					respawnEnemy, 
					hitRangeL, 
					target, 
					respawnPlayer,
					lowestGroundObject;

	public float speed = 2.0f;

	public Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		hasSpawn = false;
		walkingLeft = false;
		walkingRight = false;
	}
	// Update is called once per frame
	void Update () {
		EnemyBehaviour ();
		Raycasting ();

		// The Enemy will only walk when he is in the range of the main Camera
		if (hasSpawn == false) {
			if(GetComponent<Renderer>().IsVisibleFrom(Camera.main)){
				Spawn();
			}
		}
		if (transform.position.y < lowestGroundObject.position.y) {
			Destroy(gameObject);
		}
		if (isDead == true) {
			anim.SetTrigger("Dead");		
		}
	}

	public void Respawn(){
		transform.position = respawnEnemy.position;
		transform.rotation = respawnEnemy.rotation;
		hasSpawn = false;
		walkingLeft = false;
		walkingRight = false;
	}
	// spawn the enemy
	private void Spawn(){
		hasSpawn = true;
		walkingLeft = true;
	}

	// checking if the enemy is on the ground and if the enemy sees the player
	void Raycasting(){
		Debug.DrawLine (this.transform.position, groundEnd.position, Color.green);
		isOnGround = Physics2D.Linecast(this.transform.position, groundEnd.position, 1 << LayerMask.NameToLayer("Ground"));

		Debug.DrawLine (this.transform.position, hitRangeL.position, Color.yellow);
		hitWallL = Physics2D.Linecast(this.transform.position, hitRangeL.position, 1 << LayerMask.NameToLayer("Ground"));
	}

	// the behaviour of the enemy
	void EnemyBehaviour(){
		//todo: different enemy behaviour has to walk randomly towards different sides


		// walk towards the player 
		if (walkingLeft == true && walkingRight == false ) {
			anim.SetTrigger("Walk");
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 180);
		}

			// if there is a wall in his path turn around 
			if (hitWallL == true) {
				walkingLeft = false;
				walkingRight = true;
				transform.eulerAngles = new Vector2(0,0);

				if (walkingRight == true && walkingLeft == false) {
					speed = -speed;
					transform.Translate (Vector2.right * speed * Time.deltaTime);
					hitWallL = false;
				}
			}
		
			
	}
}
