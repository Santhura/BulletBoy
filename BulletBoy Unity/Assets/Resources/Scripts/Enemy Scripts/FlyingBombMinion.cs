using UnityEngine;
using System.Collections;

public class FlyingBombMinion : EnemyAI {


    public override void Awake()
    {
        base.Awake();
        enemyType = "Flying";
    }

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}
}
