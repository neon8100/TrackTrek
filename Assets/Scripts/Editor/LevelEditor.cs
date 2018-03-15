using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    [MenuItem("Window/Level Editor")]
    public static void Init()
    {
        EditorWindow.GetWindow(typeof(LevelEditor), false, "Level Editor Window");
    }

    private void OnEnable()
    {
        scrollPos = new Vector2();
    }

    public TileLayoutAsset level;
    public Vector2 map;

    public Color[] buttonColors = new Color[4];

    bool clear;

    public void OnGUI()
    {
        buttonColors[0] = Color.green;
        buttonColors[1] = Color.yellow;
        buttonColors[2] = Color.grey;
        buttonColors[3] = Color.blue;







        level = (TileLayoutAsset) EditorGUILayout.ObjectField("Level To Edit", level, typeof(TileLayoutAsset), false);

        if (level == null) { EditorGUILayout.LabelField("Add Asset to get started"); return; }
        if (GUILayout.Button("Load Map Size"))
        {
            map = level.mapSize;
        }
        map = EditorGUILayout.Vector2Field("Map Size", map);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate")) { AdjustSize(); }
       
        if (clear)
        {
            if (GUILayout.Button("CONFIRM"))
            {
                clear = false;
                Clear();
            }
        }
        else if (GUILayout.Button("Clear"))
        {

            clear = true;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("Box");
        DrawLayoutInput();
        EditorGUILayout.EndVertical();

    }

    Vector2 scrollPos;

    void DrawLayoutInput()
    {
        int count = 0;

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for(int y=0; y<level.mapSize.y; y++)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            for (int x=0; x<level.mapSize.x; x++)
            {
                
                int tile = (int)level.tiles[count];

                GUI.backgroundColor = buttonColors[tile];

                if (GUILayout.Button(tile.ToString(), GUILayout.Width(25)))
                {
                    tile++;
                    if (tile > 3) { tile = 0; }

                    level.tiles[count] = (TileTypes)tile;
                }

                count++;
                
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();

    }

    void AdjustSize()
    {
        level.mapSize = map;

        if (level.tiles.Length < (map.x * map.y))
        {
            List<TileTypes> temp = new List<TileTypes>();
            foreach(TileTypes t in level.tiles)
            {
                temp.Add(t);
            }

            level.tiles = new TileTypes[(int)(map.x * map.y)];
            for(int i=0; i<temp.Count; i++)
            {
                level.tiles[i] = temp[i];
            }
        }

    }

    void Clear()
    {
        for (int i=0; i<level.tiles.Length; i++)
        {
            level.tiles[i] = TileTypes.Grass;
        }
    }



}
