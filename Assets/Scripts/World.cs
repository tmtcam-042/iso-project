
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

    private Tile hoveredTile;
    private List<Vector2Int> adjTiles;

    public Color hoverColor = Color.red;
    public Color adjacentColor = Color.blue;
    public Color normColor = Color.white;


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

                // Attach collider to hexTile
                MeshCollider tileCollider = hexTile.AddComponent<MeshCollider>();
                tileCollider.convex = true;
                tileCollider.isTrigger = true;
                tileCollider.sharedMesh = hexTile.GetComponent<Transform>().GetChild(0).GetComponent<MeshFilter>().sharedMesh;
            }
        }
    }

    // Function to correctly tile all hexes in world view
    public Vector3 CalculateHexPosition(int i, int j) {
      
      float zPos = i * hexVerticalSpacing; // X axis
      float xPos = j * hexHorizontalSpacing; // Y axis
      float yPos = 0.0f;

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

    public void Update()
  {
      // Check for mouse raycast hit
      RaycastHit hit;
      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
      {
          // Check if hit object has Tile component
          Tile hitTile = hit.collider.GetComponent<Tile>();
          if (hitTile != null)
          {
              // Check if hovered tile has changed
              if (hoveredTile != hitTile)
              {
                  // Reset previous hovered tile color
                  if (hoveredTile != null)
                  {
                      hoveredTile.SetTileColor(normColor);
                  }

                  // Update hovered tile
                  hoveredTile = hitTile;
                  hoveredTile.SetTileColor(hoverColor);

                  // Get adjacent tiles
                  adjTiles = Utils.HexAdjacency(hoveredTile.x, hoveredTile.y, x, y);
                  foreach (Vector2Int adjIndex in adjTiles)
                  {
                      Tile adjacentTile = hexTiles[adjIndex.x, adjIndex.y].GetComponent<Tile>();
                      adjacentTile.SetTileColor(adjacentColor);
                  }
              }
          }
      }
      else
      {
          // Reset hovered tile color
          if (hoveredTile != null)
          {
              hoveredTile.SetTileColor(normColor);
              hoveredTile = null;
          }

          // Reset adjacent tiles color
          if (adjTiles != null)
          {
              foreach (Vector2Int adjIndex in adjTiles)
              {
                  Tile adjacentTile = hexTiles[adjIndex.x, adjIndex.y].GetComponent<Tile>();
                  adjacentTile.SetTileColor(normColor);
              }
              adjTiles.Clear();
          }
      }
  }
}