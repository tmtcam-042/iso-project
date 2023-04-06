using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{
    public int x; // Length of hexmap
    public int y; // Width of hexmap
    public int size; // Radius of hexmap
    public GameObject hexagonalPrismTilePrefab;

    public float hexHorizontalSpacing = 0.55f; // Horizontal spacing between hexes
    public float hexVerticalSpacing = 1.0f; // Vertical spacing between hexes

    private GameObject[,] hexTiles; // Array to store hexagonal prism tiles
    private string hexTag = "HexTile";

    private Tile hoveredTile;
    private List<GameObject> adjTiles;

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
        int arraySize = size * 2 + 1;
        hexTiles = new GameObject[arraySize, arraySize]; // Null array

        // Loop through x and y to create hexagonal prism tiles
        for (int q = 0; q < arraySize ; q++) // 'row' of hex
        {
            // Calculate array values for hex coordinate space.
            // Reference: https://www.redblobgames.com/grids/hexagons/#map-storage
            int minR = 0;
            int maxR = 0;
            int offset = size - q;

            if (Math.Sign(offset) == 1) 
            {
                minR = offset;
                maxR = arraySize;
            }
            else if (Math.Sign(offset) == 0)
            {
              minR = 0;
              maxR = arraySize;
            }
            else if (Math.Sign(offset) == -1)
            {
              minR = 0;
              maxR = arraySize + offset;
            }

            for (int r = minR; r < maxR; r++) // axial direction of hex
            {
                // Instantiate hexagonal prism tile and set its position
                GameObject hexTile = Instantiate(hexagonalPrismTilePrefab, CalculateHexPosition(r, q), Quaternion.Euler(90, 30, 0));
                hexTile.transform.SetParent(transform); // Set hexTile as child of WorldMap
                hexTile.name = "Tile_" + r + "_" + q; // Set name of hexTile
                hexTile.tag = hexTag;
                hexTiles[r, q] = hexTile; // Store hexTile in hexTiles array

                // Attach Tile script to hexTile
                Tile tileScript = hexTile.AddComponent<Tile>();
                tileScript.q = q; // Set x index of tile
                tileScript.r = r; // Set y index of tile

                // Attach collider to hexTile
                MeshCollider tileCollider = hexTile.AddComponent<MeshCollider>();
                tileCollider.convex = true;
                tileCollider.isTrigger = true;
                tileCollider.sharedMesh = hexTile.GetComponent<Transform>().GetChild(0).GetComponent<MeshFilter>().sharedMesh;
            }
        }
    }

    // Function to correctly tile all hexes in world view.
    // Q represents the 'row' of the hex, starting from 0 at the most vertical
    // R represents the 'axial position' of the hex, and runs along a specific axis of the hex
    public Vector3 CalculateHexPosition(int r, int q) {

      
      float zPos = q * hexVerticalSpacing; // Vertical Axis

      float originOffset = -(1 + size)*hexHorizontalSpacing; // Pin beneath anchor sphere
      float xPos = -((float)r + (float)q/2)*hexHorizontalSpacing - originOffset; // Radial Axis

      float yPos = 0.0f;


      return new Vector3(xPos, yPos, zPos);
    }
    
    // Given a target tile and a range, returns all targets within that range
    public List<GameObject> TilesInRange(Tile target, int range)
    {
      List<GameObject> tilesInRange = new List<GameObject>();
      int Q = target.q + range;
      int R = target.r + range;
      for (int i = -Q; i <= Q; i++)
      {
        for (int j = Math.Max(-Q, -target.q-Q); j <= Math.Min(Q, -target.q+Q); j++)
        {
          if (TileAt(i, j))
          {
            tilesInRange.Add(TileAt(i, j));
          }
        }
      }
      return tilesInRange;
    }

    // Returns true if tile is at given co-ords, false otherwise
    public GameObject TileAt(int r, int q)
    {
      try
      {
        return hexTiles[r, q];
      }
      catch (System.Exception)
      {
        return null;
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
                  // Reset tile colors
                  foreach(var tile in hexTiles.here(x => x != null))
                  {
                    if (tile == null) { continue; }
                    tile.GetComponent<Tile>().SetTileColor(normColor);
                  }

                  // Update hovered tile
                  hoveredTile = hitTile;
                  hoveredTile.SetTileColor(hoverColor);

                  // Get adjacent tiles
                  adjTiles = TilesInRange(hoveredTile, 1);
                  //adjTiles = Utils.HexAdjacency(hoveredTile.x, hoveredTile.y, x, y);
                  foreach (GameObject adjacentTile in adjTiles)
                  {
                      adjacentTile.GetComponent<Tile>().SetTileColor(adjacentColor);
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
              foreach (var adjacentTile in adjTiles)
              {
                  if (adjacentTile == null) { continue; }
                  adjacentTile.GetComponent<Tile>().SetTileColor(normColor);
              }
              adjTiles.Clear();
          }
      }
    }

    public void DebugHexMap() {
      foreach(var item in hexTiles)
      {
        print(item);
      }
    }

}