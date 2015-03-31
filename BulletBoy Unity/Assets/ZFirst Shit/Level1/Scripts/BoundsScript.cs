using UnityEngine;
using System.Collections;

public class BoundsScript : MonoBehaviour {

	public Transform target;
	public bool isPlayerBounds = false;

	//Check if the player collides with the bounds object
	// and bounds the player to a specific target
	void OnCollisionEnter2D(Collision2D collision){
		Player player = collision.gameObject.GetComponent<Player> ();

		if (player != null) {
			isPlayerBounds = true;		
		}
		if (isPlayerBounds) {
			player.GetComponent<Rigidbody2D>().velocity = (target.transform.position - this.transform.position);

			if(player.isOnGround == true){
				player.GetComponent<Rigidbody>().velocity = Vector2.zero;
			}
		}
	}
}
