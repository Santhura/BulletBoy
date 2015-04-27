using UnityEngine;
using System.Collections;

public class PlaneGrid : MonoBehaviour {

    int cols = 80, rows = 80;
    public GameObject gridLines;
	// Use this for initialization
	void Start () {
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                Instantiate(gridLines, new Vector2(-27 + y , 0), Quaternion.identity);
                Instantiate(gridLines, new Vector2(0, 40 - x), new Quaternion(transform.rotation.x, transform.rotation.y,90, 90));
            }
        }
	}
}
