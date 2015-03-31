using UnityEngine;
using System.Collections;

public class FlyingEnemyScript : MonoBehaviour {

	public float speed = -0.2f,
				dropRate = 0.25f;

	public Transform respawnFlyingEnmey,
					dropAttackPrefab;
	public bool hasSpawn,
				movingLeft;

	private float dropCoolDown;
	void Start(){
		hasSpawn = false;
		movingLeft = false;
		dropCoolDown = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		FlyingBehaviour ();
		Attack ();

		if (dropCoolDown > 0) {
			dropCoolDown -= Time.deltaTime;
		}
		// checks if flyingEnemy is in screen
		if (hasSpawn == false) {
		
			if(GetComponent<Renderer>().IsVisibleFrom(Camera.main)){
				Spawn();
			}
		}
	}

	public void Attack (){

		if (canDrop) {
			dropCoolDown = dropRate;

			// creates new dropAttack object
			var dropTransform = Instantiate(dropAttackPrefab) as Transform;

			// gives the dropAttack the position of the flyingEnemy
			dropTransform.position = transform.position;
			// checks if the drop hits
			DropAttackScript drop = dropTransform.gameObject.GetComponent<DropAttackScript>();
			if(drop != null){
				drop.isPlayerHit = true;
			}
		}
	}

	// the time when the dropAttack can drop
	public bool canDrop 
	{
		get 
		{
			return dropCoolDown <= 0.0f;
		}
	}

	// the behaviour of the flying enemy
	void FlyingBehaviour(){
		if (movingLeft == true) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
	}

	private void Spawn(){
		hasSpawn = true;
		movingLeft = true;
	}

	public void Respawn(){
		transform.position = respawnFlyingEnmey.position;
		transform.rotation = respawnFlyingEnmey.rotation;
		hasSpawn = false;
		movingLeft = false;
	}
}
