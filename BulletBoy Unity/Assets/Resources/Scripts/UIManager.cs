using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public GameObject image_Heart;
    public static bool gotHeart = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gotHeart)
        {
            image_Heart.SetActive(true);
        }
        else
        {
            image_Heart.SetActive(false);
        }
	}
}
