using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {


    // todo: 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Level completed");
        }
    }
}
