using UnityEngine;
using System.Collections;

public class PlaneGrid : MonoBehaviour {

    int cols = 50, rows = 50;
    public GameObject gridLines;
	// Use this for initialization
	void Start () {
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                Instantiate(gridLines, new Vector2(-18 + y , 0), Quaternion.identity);
                Instantiate(gridLines, new Vector2(0, 33 - x), new Quaternion(transform.rotation.x, transform.rotation.y,90, 90));
            }
        }
	}
}
