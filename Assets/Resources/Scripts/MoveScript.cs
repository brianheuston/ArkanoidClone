using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
    public float speed = 3.0f;
    private float originalY;
    private float maxX = 85; // Default value

	// Use this for initialization
	void Start () {
        originalY = transform.position.y;
        GameObject leftBorder = GameObject.Find("border_left");
        GameObject rightBorder = GameObject.Find("border_right");

        // The game boundaries are:
        // ((Left border + border width + racket width), (Right border - border width - racket width))
        if (leftBorder != null && rightBorder != null)
        {
            float newMaxX = rightBorder.transform.position.x;
            if (rightBorder.collider2D != null)
            {
                newMaxX -= ((BoxCollider2D)rightBorder.collider2D).size.x / 2.0f;
            }

            newMaxX -= ((BoxCollider2D)transform.collider2D).size.x / 2.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-speed, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(speed, 0));
        }

        if (transform.position.y != originalY)
        {
            transform.Translate(new Vector2(0, originalY - transform.position.y));
        }

        if (transform.position.x > maxX)
        {
            transform.Translate(new Vector2((maxX - transform.position.x), 0));
        }
        if (transform.position.x < -maxX)
        {
            transform.Translate(new Vector2(-transform.position.x - maxX, 0));
        }
    }
}
