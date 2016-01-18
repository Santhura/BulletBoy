using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour {

    PlayerEngine player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerEngine>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Heart")
            {
                player.heartAmount = 1;
                UIManager.gotHeart = true;
                Destroy(gameObject);
            }
        }
    }
}
