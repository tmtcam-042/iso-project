using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        World world = (World)target; // Get the World (WorldMap) script reference

        if (GUILayout.Button("Regenerate Hex Map"))
        {
            world.GenerateHexMap(); // Call the GenerateHexMap() method on button click
        }

        if (GUILayout.Button("Destroy All Hex Tiles"))
        {
            world.DestroyAllHexTiles(); // Call the DestroyAllHexTiles() method on button click
        }

    }
}
