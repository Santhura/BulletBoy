using UnityEngine;
using System.Collections;

public class SpikeEnemy : Enemy {

	public float speedL;
	//public Transform lowestGroundObject;
	// Use this for initialization


	void Start () {
//	anim = GetComponent<Animator> ();
		hasSpawn = false;
		walkingLeft = false;
		speedL = -3.5f;
	}
	
	// Update is called once per frame
	void Update () {
		SpikeEnemyBehaviour ();
		Raycasting ();

		if (hasSpawn == false) {
			
			if(GetComponent<Renderer>().IsVisibleFrom(Camera.main)){
				Spawn();
			}
			else
				if ( GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false){
					Respawn();
				}
		}
		if (transform.position.y < lowestGroundObject.position.y) {
			Destroy(gameObject);
			
		}
	}
	void Raycasting(){
		Debug.DrawLine (this.transform.position, groundEnd.position, Color.blue);
		isOnGround = Physics2D.Linecast (this.transform.position, groundEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
	}

	void SpikeEnemyBehaviour(){
		if (walkingLeft) {
			anim.SetTrigger ("Ride");
			transform.Translate (Vector2.right * speedL * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 0);		
		}
	}

	void Spawn(){
		hasSpawn = true;
		walkingLeft = true;
	}

	public void Respawn(){
			transform.position = respawnEnemy.position;
			transform.rotation = respawnEnemy.rotation;
			hasSpawn = false;
			walkingLeft = false;
	}
}
