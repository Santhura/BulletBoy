using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    [Header("Enemy Spawn")]
    public GameObject enemy;
    private GameObject initEnemy;
    private Vector3 spawnPos;
    public float spawnTime = 5;
    private float prevSpawnTime;
    public bool canSpawn { get; set; }
    public static int spawnCounter = 0;

	// Use this for initialization
    void Start()
    {
        prevSpawnTime = spawnTime;
        canSpawn = true;
	}

    void Update()
    {
        if (canSpawn)
        {
            if (spawnCounter < 10)
            {
                spawnTime -= Time.deltaTime;

                if (spawnTime < 1)
                {
                    spawnPos = new Vector3(Random.Range(-4, 5), 0, Random.Range(-2, 15));
                    initEnemy = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
                    spawnTime = prevSpawnTime;
                    spawnCounter++;
                }
            }
        }
    }
}
