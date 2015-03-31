using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float speed = 4.0f, 
				runningSpeed = 7.0f, 
				jumpForce = 300.0f;
	float jumpTime,
		  jumpDelay = .7f;

	public int life = 3;

	public bool isOnGround = false,
				hitEnemy = false,
				isPlayerDead = false;

		   bool jumped,
				doubleJump;

	public Transform groundedEnd, 
					respawnPosition, 
					lowestGroundObject,
					hitRange,
					hitRangeLeft,
					hitRangeRight,
					beginHitLeft,
					beginHitRight;

	RaycastHit2D whatIHit;

	Animator anim;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();		
		}
		Movement ();
		Raycasting ();
		KillEnemy ();


		if (transform.position.y < lowestGroundObject.position.y) {
			life--;
			Respawn();

		}
	/*	if (isOnGround) {
			jumps = 0;	
			jumped = false;
			doubleJump = false;
		}*/

	}

	// check the collision between player and enemy
	// 
	void OnCollisionEnter2D(Collision2D collision){
		
		Enemy enemy = collision.gameObject.GetComponent<Enemy> ();
		FlyingEnemyScript flyEnemy = collision.gameObject.GetComponent<FlyingEnemyScript> ();
		SpikeEnemy spikeEnemy = collision.gameObject.GetComponent<SpikeEnemy> ();

		if (enemy != null || flyEnemy != null || spikeEnemy != null) {
			isPlayerDead = true;		
		}

		if (isPlayerDead) {
			life--;
			isPlayerDead = false;
			Respawn();// Later when level and the menu are made dont respawn this object, but return to the world map
			enemy.Respawn();
			flyEnemy.Respawn();
		}
		if(life <= 0){ // Later when level and the menu are made dont destroy this object, but return to the world map and show the game over screen
			Destroy(gameObject);
		}
	}
	// checks if the player gets hit by the bullet or the drop from something
	void OnTriggerEnter2D(Collider2D collider){
		DropAttackScript dropAttack = collider.gameObject.GetComponent<DropAttackScript> ();
		FlyingEnemyScript flyEnemy = GetComponent<FlyingEnemyScript> ();
		if (dropAttack != null) {
			isPlayerDead = true;

			if(isPlayerDead){
				life--;
				isPlayerDead = false;
				Respawn();
				flyEnemy.Respawn();
				Destroy(dropAttack.gameObject);
			}
			if(life <= 0){ // Later when level and the menu are made dont destroy this object, but return to the world map and show the game over screen
				Destroy(gameObject);
			}
		}
	}

	// Respawn player to old position
	public void Respawn(){
		transform.position = respawnPosition.position;
		transform.rotation = respawnPosition.rotation;
	}

	void Raycasting(){
		Debug.DrawLine (this.transform.position, groundedEnd.position, Color.green);
		isOnGround = Physics2D.Linecast(this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer("Ground"));

		Debug.DrawLine (this.transform.position, hitRange.position, Color.blue);
		hitEnemy = Physics2D.Linecast (this.transform.position, hitRange.position, 1 << LayerMask.NameToLayer ("Enemy"));

		Debug.DrawLine (beginHitLeft.position, hitRangeLeft.position, Color.blue);
		hitEnemy |= Physics2D.Linecast (beginHitLeft.position, hitRangeLeft.position, 1 << LayerMask.NameToLayer ("Enemy"));

		Debug.DrawLine (beginHitRight.position, hitRangeRight.position, Color.blue);
		hitEnemy |= Physics2D.Linecast (beginHitRight.position, hitRangeRight.position, 1 << LayerMask.NameToLayer ("Enemy"));

		//Physics2D.IgnoreLayerCollision (8, 10);
	}

	void KillEnemy(){
		if (hitEnemy == true) {
			whatIHit = Physics2D.Linecast (this.transform.position, hitRange.position, 1 << LayerMask.NameToLayer ("Enemy"));
			Destroy (whatIHit.collider.gameObject);
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce * 1.5f);

		}
	}

	void Jump(){
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
		jumpTime = jumpDelay;
		//jumps++;
		jumped = true;
	}
	//The movement of the player
	void Movement(){
		anim.SetFloat("speed", Mathf.Abs (Input.GetAxis("Horizontal")));
		// go Right
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,0);	// This sets the rotation of the gameObject
		}
		//Go Left
		//when we flip the sprite we dont need speed minus because it is also flipped as well
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,180);	// This sets the rotation of the gameObject
		}


		// Let the player jump
		if (Input.GetKeyDown (KeyCode.Space) && isOnGround && jumped == false ) {
			Jump ();
			anim.SetTrigger ("Jump");

			/*if(jumps == 2 && doubleJump == false && jumped && isOnGround == false){
				anim.SetTrigger("Jump2");
				doubleJump = true;
				jumped = false;
				jumpTime = jumpDelay;*/
		//	}

		}
		jumpTime -= Time.deltaTime;
		if (jumpTime <= 0 && isOnGround && jumped) {
			anim.SetTrigger("Land");
			jumped = false;
		}
		/*if (jumpTime <= 0 && doubleJump && jumped == false && isOnGround) {
			anim.SetTrigger("Land");
			doubleJump = false;
		}*/

		// player punches
		if(Input.GetKeyDown(KeyCode.X)){
			anim.SetTrigger("Punch");
		}

		// checks if the shift key is pressed and the walking key is pressed for the running animation
		anim.SetBool ("shift", false);

		if (Input.GetKey (KeyCode.LeftShift) && Input.GetAxisRaw ("Horizontal") > 0 && isOnGround) {
			anim.SetBool ("shift", true);
			transform.Translate(Vector2.right * runningSpeed * Time.deltaTime);		
			transform.eulerAngles = new Vector2(0,0);
		}
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetAxisRaw ("Horizontal") < 0 && isOnGround ) {
			anim.SetBool ("shift", true);
			transform.Translate(Vector2.right * runningSpeed * Time.deltaTime);		
			transform.eulerAngles = new Vector2(0,180);
		}
	}
}
