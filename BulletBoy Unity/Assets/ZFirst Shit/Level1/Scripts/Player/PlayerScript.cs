using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float _speed = 4, _runningSpeed = 7, _jumpForce = 450, _accelaration = 1f;
	private int _jumps = 0, _maxJumps = 2;
	private Animator _anim;
	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) 
			Application.Quit ();

		Movement ();
	}
    // maybe change the movement of the player to use rigidbody velocity
	private void Movement(){ 
		_anim.SetFloat("speed",Mathf.Abs(Input.GetAxis("Horizontal")));
        _anim.SetBool("shift", false);
		if (Input.GetAxisRaw ("Horizontal") > 0) {
            _speed += _accelaration * Time.deltaTime;
			transform.Translate (Vector2.right * _speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 0);

            if (_speed >= _runningSpeed) {
               _anim.SetBool("shift", true);
               _speed = _runningSpeed;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _speed += _accelaration * Time.deltaTime;
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
            if (_speed >= _runningSpeed)
            {
                _anim.SetBool("shift", true);
                _speed = _runningSpeed;
            }
        }
        else
        {
            _speed = 4;
        }

		/*
		if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetKey (KeyCode.LeftShift)) {
			_anim.SetBool("shift", true);
			transform.Translate (Vector2.right * _runningSpeed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 0);
		} 
		else if(Input.GetAxisRaw ("Horizontal") < 0 && Input.GetKey (KeyCode.LeftShift)){
			_anim.SetBool("shift", true);
			transform.Translate (Vector2.right * _runningSpeed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 180);	
		}*/

		if (Input.GetKeyDown (KeyCode.Space) && _jumps < _maxJumps) {
			Jump();		
		}
	}

	private void Jump(){
		GetComponent<Rigidbody2D>().AddForce (Vector2.up * _jumpForce);
		_jumps++;
		_anim.SetTrigger("Jump");

		if (_jumps > 0)
				_jumpForce /= 2;
	}

	void OnCollisionEnter2D(Collision2D other){
		_jumps = 0;
		_anim.SetTrigger("Land");
		_jumpForce = 450;
	}
}
