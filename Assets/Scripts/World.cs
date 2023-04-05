using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{
    public int x; // Length of hexmap
    public int y; // Width of hexmap
    public GameObject hexagonalPrismTilePrefab;

    private GameObject[,] hexTiles; // Array to store hexagonal prism tiles
    private string hexTag = "HexTile";


    // Function to generate hexmap
    public void GenerateHexMap()
    {
        // Destroy old hexmap if exists
        if (hexTiles != null)
        {
            DestroyAllHexTiles();
        }

        // Create new hexmap
        hexTiles = new GameObject[x, y];

        // Loop through x and y to create hexagonal prism tiles
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                // Instantiate hexagonal prism tile and set its position
                GameObject hexTile = Instantiate(hexagonalPrismTilePrefab, new Vector3(i, 0, j), Quaternion.Euler(90, 0, 0));
                hexTile.transform.SetParent(transform); // Set hexTile as child of WorldMap
                hexTile.name = "Tile_" + i + "_" + j; // Set name of hexTile
                hexTile.tag = hexTag;
                hexTiles[i, j] = hexTile; // Store hexTile in hexTiles array

                // Attach Tile script to hexTile
                Tile tileScript = hexTile.AddComponent<Tile>();
                tileScript.x = i; // Set x index of tile
                tileScript.y = j; // Set y index of tile
            }
        }
    }

    // Function to destroy all children hex tiles when called
    public void DestroyAllHexTiles()
    {
        // Destroy all children hex tiles
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag(hexTag))
            {
                DestroyImmediate(child.gameObject); // Destroy the child GameObject
            }
        }
    }
}