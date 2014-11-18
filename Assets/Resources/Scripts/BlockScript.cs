using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {
    public bool IsDestructible = true;
    public int NumberOfHitsToDestroy = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (IsDestructible && col.gameObject.name == "ball" && --NumberOfHitsToDestroy == 0)
        {
            Destroy(gameObject);
        }
    }
}
