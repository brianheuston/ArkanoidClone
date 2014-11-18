using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
    public float speed = 3.0f;
    private float originalY;
    private float maxX = 85; // Default value
    private bool isInStickyMode = true;
    private bool hasStickyPowerup = false;

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

        // Initially in sticky mode
        isInStickyMode = true;
        hasStickyPowerup = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsInStickyMode()
    {
        return isInStickyMode;
    }

    void FixedUpdate()
    {
        Vector2 vector = new Vector2();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector.x -= speed;
            //transform.Translate(new Vector2(-speed, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector.x += speed;
            //transform.Translate(new Vector2(speed, 0));
        }

        // Ensure we stay in y-boundary
        if (transform.position.y != originalY)
        {
            vector.y += originalY - transform.position.y;
            //transform.Translate(new Vector2(0, originalY - transform.position.y));
        }

        // Ensure we stay in x-boundary
        if (transform.position.x + vector.x > maxX)
        {
            vector.x = (maxX - transform.position.x);
            //transform.Translate(new Vector2((maxX - transform.position.x), 0));
        }
        if (transform.position.x + vector.x < -maxX)
        {
            vector.x = -maxX - transform.position.x;
            //transform.Translate(new Vector2(-transform.position.x - maxX, 0));
        }

        transform.Translate(vector);

        GameObject obj = GameObject.Find("ball");
        if (obj != null && ((BallScript)obj.GetComponent<BallScript>()).IsBallStuck())
        {
            obj.transform.Translate(vector);

            if (obj.transform.position.x - transform.position.x != ((BallScript)obj.GetComponent<BallScript>()).GetOffsetPosition())
            {
                float t = ((BallScript)obj.GetComponent<BallScript>()).GetOffsetPosition();
                obj.transform.position = new Vector2(transform.position.x + ((BallScript)obj.GetComponent<BallScript>()).GetOffsetPosition(), obj.transform.position.y);
            }
        }

        if (Input.GetKey(KeyCode.Space) && obj != null && ((BallScript)obj.GetComponent<BallScript>()).IsBallStuck())
        {
            ((BallScript)obj.GetComponent<BallScript>()).FlipStuck();
            if (!hasStickyPowerup)
            {
                isInStickyMode = false;
            }
        }
    }
}
