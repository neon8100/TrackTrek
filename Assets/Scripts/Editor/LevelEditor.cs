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

    bool toggleLayouts;

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
        toggleLayouts = GUILayout.Toggle(toggleLayouts, "Toggle Layout");
        Debug.Log(toggleLayouts);
        if (toggleLayouts)
        {
            if (GUILayout.Button("Generate Layouts"))
            {
                 int size = (int)level.mapSize.y * (int)level.mapSize.x;
                    level.layouts = new LayoutType[size];
                    for (int i = 0; i < size; i++)
                    {
                        level.layouts[i] = (LayoutType)0;
                    }
                
            }
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

        EditorGUI.BeginChangeCheck();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for(int y=0; y<level.mapSize.y; y++)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            for (int x=0; x<level.mapSize.x; x++)
            {
                
                int tile = (int)level.tiles[count];
                int layout = (int)level.layouts[count];

                GUI.backgroundColor = buttonColors[tile];

                if (toggleLayouts)
                {
                    string icon = "";
                    switch ((LayoutType)layout)
                    {
                        case LayoutType.None:
                            icon = "";
                            break;
                        case LayoutType.Wood:
                            icon = "[W]";
                            break;
                        case LayoutType.TrackHorizontal:
                            icon = "-";
                            break;
                        case LayoutType.TrackVertical:
                            icon = "|";
                            break;
                        case LayoutType.TrackLE:
                            icon = "|_";
                            break;
                        case LayoutType.TrackLW:
                            icon = "_|";
                            break;
                        case LayoutType.TrackNE:
                            icon = "|^^";
                            break;
                        case LayoutType.TrackNW:
                            icon = "^^|";
                            break;
                        case LayoutType.TrackCross:
                            icon = "+";
                            break;
                        case LayoutType.TrackStart:
                            icon = "[S]";
                            break;
                        case LayoutType.TrackEnd:
                            icon = "[E]";
                            break;
                    }

                    if (GUILayout.Button(icon, GUILayout.Width(25)))
                    {
                        layout++;
                        if (layout > 10) { layout = 0; }

                        level.layouts[count] = (LayoutType)layout;
                    }
                }
                else
                {
                    if (GUILayout.Button(tile.ToString(), GUILayout.Width(25)))
                    {
                        tile++;
                        if (tile > 3) { tile = 0; }

                        level.tiles[count] = (TileTypes)tile;
                    }
                }

                count++;
                
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();

        if(EditorGUI.EndChangeCheck()){
            EditorUtility.SetDirty(level);
        }
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
