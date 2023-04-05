using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{
    public int x; // Length of hexmap
    public int y; // Width of hexmap
    public GameObject hexagonalPrismTilePrefab;

    public float hexHorizontalSpacing = 0.75f; // Horizontal spacing between hexes
    public float hexVerticalSpacing = 1.0f; // Vertical spacing between hexes

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
                GameObject hexTile = Instantiate(hexagonalPrismTilePrefab, CalculateHexPosition(i, j), Quaternion.Euler(90, 0, 0));
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

    // Function to correctly tile all hexes in world view
    public Vector3 CalculateHexPosition(int i, int j) {
      

      float xPos = j * hexHorizontalSpacing;
      float yPos = 0.0f;
      float zPos = i * hexVerticalSpacing;


      if (j % 2 == 0) 
      {
          zPos += hexVerticalSpacing/2;
      }


      return new Vector3(xPos, yPos, zPos);
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