using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public TileLayoutAsset levelLayout;

    public GameObject player1;
    public GameObject player2;

    public void Awake()
    {
        GenerateLevel();
    }


    GameObject map;
    void GenerateLevel()
    {
        map = new GameObject("Map");

        int count = 0;

        List<GameObject> tileList = levelLayout.GetTiles();

        //Layout the map based on the mapsize vector
        for(int y=0; y<levelLayout.mapSize.y; y++)
        {  for(int x=0; x<levelLayout.mapSize.x; x++)
            {
                int xPos = x;
                int yPos = -y;
                AddTileToLevel(new Vector2(xPos, yPos), tileList[count]);
                count++;
            }
        }

        player1.transform.position = new Vector3(levelLayout.mapSize.x/ 2, -levelLayout.mapSize.y / 2);
        player2.transform.position = player1.transform.position + new Vector3(2, 2, 0);

    }

    void AddTileToLevel(Vector2 pos, GameObject tile)
    {

        GameObject t = GameObject.Instantiate(tile);
        t.name = pos.ToString();
        t.transform.SetParent(map.transform);
        t.transform.position = pos;

    }

}
