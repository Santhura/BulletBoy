using UnityEngine;
using System.Collections;

public class CaveTrigger : MonoBehaviour
{


    public SpriteRenderer alphaMountain;
    public bool inCave = false;
    
    private float alphaColor = 1;
    private float numberTrigger;
    public CircleCollider2D playerCircleCol;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), playerCircleCol);
        if (inCave)
        {
            alphaColor -= 0.3f * Time.deltaTime;
            if (alphaColor <= 0)
            {
                alphaColor = 0;
            }
        }
        else
        {
            alphaColor += 0.3f * Time.deltaTime;
            if (alphaColor >= 1)
            {
                alphaColor = 1;
            }
        }
        alphaMountain.color = new Color(1, 1, 1, alphaColor);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (numberTrigger == 0)
            {
                inCave = true;
                numberTrigger = 1;
            }
            else if (numberTrigger >= 1)
            {
                inCave = false;
                numberTrigger = 0;
            }
        }
    }
}
