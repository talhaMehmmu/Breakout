using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject enemyBrick;
    public Gradient gradient;
    public void Awake()
    {
        GenerateLevel();
    }
    public void GenerateLevel()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newBrick = Instantiate(enemyBrick, transform);
                // newBrick.transform.position = transform.position + new Vector3(i*offset.x, j*offset.y, 0);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x-1) * .5f - i) * offset.x, j*offset.y, 0);
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j/(size.y-1));
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
