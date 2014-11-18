using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public float speed = 100.0f;
    private Vector2 storedVelocity;
    private bool isBallStuck;
    private float stuckOffsetX;

	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.Find("racket");
        transform.position = new Vector3(obj.transform.position.x,
                                         obj.transform.position.y + ((BoxCollider2D)obj.collider2D).size.y / 2.0f + 
                                            ((BoxCollider2D)this.collider2D).size.y / 2.0f,
                                         -0.0001f);
        storedVelocity = new Vector2(0.5f, 1) * speed;
        isBallStuck = true;
        //rigidbody2D.velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
    }

    public bool IsBallStuck()
    {
        return isBallStuck;
    }

    public void FlipStuck()
    {
        isBallStuck = !isBallStuck;

        if (!isBallStuck)
        {
            rigidbody2D.velocity = storedVelocity;
        }
    }

    public float GetOffsetPosition()
    {
        return stuckOffsetX;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "racket")
        {
            float x = HitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.x);

            Vector2 dir = new Vector2(x, 1).normalized * speed;

            storedVelocity = dir;

            GameObject racket = GameObject.Find("racket");
            if (!((MoveScript)racket.GetComponent<MoveScript>()).IsInStickyMode())
            {
                rigidbody2D.velocity = storedVelocity;
            } 
            else
            {
                rigidbody2D.velocity = new Vector2();
                isBallStuck = true;

                stuckOffsetX = transform.position.x - col.transform.position.x;
            }
        }
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }
}
