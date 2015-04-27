using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform player;
    public bool isFollowing { get; set; }
    public Vector2 margin, smoothing;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
        isFollowing = true;
	}
	
	// Update is called once per frame
	void Update () {
        var x = transform.position.x;
        var y = transform.position.y;

        if (isFollowing)
        {
            if (Mathf.Abs(x - player.position.x) > margin.x)
            {
                x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
            }
            if (Mathf.Abs(y - player.position.y) > margin.y)
            {
                y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
            }
        }

        transform.position = new Vector3(x, y, transform.position.z);
	}
}
