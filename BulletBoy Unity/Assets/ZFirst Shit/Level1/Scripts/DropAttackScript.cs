using UnityEngine;
using System.Collections;

public class DropAttackScript : MonoBehaviour {

	public float speed = 20;

	public bool isPlayerHit = false,
				hasSpawn;

	// Use this for initialization
	void Start () {
		hasSpawn = false;
		Destroy (gameObject, 1.6f); // Destroy the object after 1.6 sec
	}

	// Update is called once per frame
	void Update () {
		if (hasSpawn == false) {
			
			if(GetComponent<Renderer>().IsVisibleFrom(Camera.main)){
				Spawn();
				transform.Translate(Vector2.up * -speed * Time.deltaTime);
			}

	}

}
	private void Spawn(){
		hasSpawn = true;
	}
}
