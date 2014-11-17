using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {
    public int blockWidth = 16;
    public int blockHeight = 8;
    public int startPositionX = -96;
    public int startPositionY = 50;

    private string[] prefabNames = { "Prefabs/block_blue",
                                     "Prefabs/block_green",
                                     "Prefabs/block_yellow",
                                     "Prefabs/block_red",
                                     "Prefabs/block_pink" };

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                GameObject instance = Object.Instantiate(Resources.Load(prefabNames[i], typeof(GameObject))) as GameObject;
                instance.transform.position = new Vector3(startPositionX + blockWidth * j, 
                    startPositionY + blockHeight * i, 
                    -0.0001f);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
