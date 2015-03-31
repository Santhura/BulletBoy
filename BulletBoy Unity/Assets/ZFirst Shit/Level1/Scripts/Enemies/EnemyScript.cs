using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float _speed = 2;
    private Animator anim;
    private bool _isWalking = false;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
        {
            Spawn();
        }
        Movement();
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
	}

    void Movement() {

        if (_isWalking)
        {
            anim.SetTrigger("Walk");
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void Spawn() {
        _isWalking = true;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Rock")
        {
            _speed = -_speed;
        }
    }
}
