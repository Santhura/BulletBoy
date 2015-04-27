using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour {

    private bool gotKey = false;
    Animator doorAnimH, doorAnimV;
	// Use this for initialization
	void Start () {
        doorAnimH = GameObject.Find("HorizontalDoor").GetComponent<Animator>();
        doorAnimV = GameObject.Find("VerticalDoor").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            gotKey = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "HorizontalDoor" && gotKey)
        {
            doorAnimH.SetTrigger("Open");
            gotKey = false;
        }
        if (other.gameObject.tag == "VerticalDoor" && gotKey)
        {
            doorAnimV.SetTrigger("Open");
            gotKey = false;
        }
    }
}