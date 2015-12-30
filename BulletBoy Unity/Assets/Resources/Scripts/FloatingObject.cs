using UnityEngine;
using System.Collections;

public class FloatingObject : MonoBehaviour {

    private float originalY;
    public float floatStrength = 1;

	// Use this for initialization
	void Start () {
        originalY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), transform.position.z);

	}
}
