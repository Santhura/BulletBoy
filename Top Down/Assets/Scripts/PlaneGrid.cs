using UnityEngine;
using System.Collections;

public class PlaneGrid : MonoBehaviour
{

    int grid = 70;
    public GameObject gridLines;
    GameObject horizontalLines, verticalLines;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < grid; i++)
        {
            verticalLines = Instantiate(gridLines, new Vector2(-35 + i, 0), Quaternion.identity) as GameObject;
            horizontalLines = Instantiate(gridLines, new Vector2(0, 35 - i), new Quaternion(transform.rotation.x, transform.rotation.y, 90, 90)) as GameObject;
            horizontalLines.transform.parent = GameObject.Find("GroundLines").transform;
            verticalLines.transform.parent = GameObject.Find("GroundLines").transform;
        }
    }
}
