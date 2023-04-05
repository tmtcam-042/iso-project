using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Given an index of a tile in a hexmap, return a list of
    // inidices that are hexagonally adjacent
    public static List<Vector2Int> HexAdjacency(int x, int y, int xDim, int yDim) {
      List<Vector2Int> adjacentTiles = new List<Vector2Int>();
      Vector2Int[] directions;

      // Hexagonally adjacent directions are different if row is even vs odd
      if (y % 2 == 0) 
      {directions = EvenHexDirections;}
      else {directions = OddHexDirections;}


      for (int i = 0; i < directions.Length; i++) 
      {
        Vector2Int adjIndex = new Vector2Int(x + directions[i].x, y + directions[i].y);
        bool validIndex = (adjIndex.x >= 0 && adjIndex.x < xDim && adjIndex.y >= 0 && adjIndex.y < yDim);

        if (validIndex) {
          adjacentTiles.Add(adjIndex);
        }
      }

      return adjacentTiles;
    }


    private static readonly Vector2Int[] EvenHexDirections = 
    {
      new Vector2Int(1, 0),  // Right
      new Vector2Int(1, -1), // Top Right
      new Vector2Int(0, -1), // Top Left
      new Vector2Int(-1, 0), // Left
      new Vector2Int(0, 1), // Bottom Left
      new Vector2Int(1, 1)   // Bottom Right
    };

    private static readonly Vector2Int[] OddHexDirections = 
    {
      new Vector2Int(1, 0),  // Right
      new Vector2Int(0, -1), // Top Right
      new Vector2Int(-1, -1), // Top Left
      new Vector2Int(-1, 0), // Left
      new Vector2Int(-1, 1), // Bottom Left
      new Vector2Int(0, 1)   // Bottom Right
    };
}
