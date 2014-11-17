using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public float speed = 100.0f;

	// Use this for initialization
	void Start () {
        rigidbody2D.velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "racket")
        {
            float x = HitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rigidbody2D.velocity = dir * speed;
        }
    }

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }
}
