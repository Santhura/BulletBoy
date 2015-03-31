using UnityEngine;
using System.Collections;

public class BombMinionScript : MonoBehaviour {

    public float _speed = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        EnemyBehaviour();
	}

    void EnemyBehaviour()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        if (_speed < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_speed > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
