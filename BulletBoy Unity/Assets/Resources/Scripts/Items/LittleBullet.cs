using UnityEngine;
using System.Collections;

public class LittleBullet : MonoBehaviour {

    Animator anim;

    private float originalY;
    public float floatStrength = 1;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        originalY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), transform.position.z);
	}
}
