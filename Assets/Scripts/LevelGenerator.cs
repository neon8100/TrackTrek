using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator instance;

    public TileLayoutAsset levelLayout;
    public MapAssets mapAssets;

    public GameObject player1;
    public GameObject player2;

    PolygonCollider2D bounds;

    public GameObject train;

    public Cinemachine.CinemachineConfiner confiner;

    public void Awake()
    {
        GenerateLevel();

        bounds = map.AddComponent<PolygonCollider2D>();
        instance = this;
    }

    private void Start()
    {
        Vector2[] b = new Vector2[4];
        b[0] = new Vector2(0, 0);
        b[1] = new Vector2(0, -levelLayout.mapSize.y);
        b[2] = new Vector2(levelLayout.mapSize.x, -levelLayout.mapSize.y);
        b[3] = new Vector2(levelLayout.mapSize.x, 0);

        bounds.points = b;
        bounds.isTrigger = true;

        confiner.m_BoundingShape2D = bounds;
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

                if (levelLayout.layouts[count] != LayoutType.None) {
                    AddTileToLevel(new Vector2(xPos, yPos), mapAssets.GetAsset(levelLayout.layouts[count]));
                }
                if(levelLayout.layouts[count] == LayoutType.TrackStart)
                {
                    AddTileToLevel(new Vector2(xPos, yPos), train);
                }

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
