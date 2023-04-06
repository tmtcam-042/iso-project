using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utils
{
    // Given an index of a tile in a hexmap, return a list of
    // inidices that are hexagonally adjacent
    // public static List<Vector2Int> HexAdjacency(int x, int y, int xDim, int yDim) {
    //   List<Vector2Int> adjacentTiles = new List<Vector2Int>();
    //   Vector2Int[] directions;

    //   // Hexagonally adjacent directions are different if row is even vs odd
    //   if (y % 2 == 0) 
    //   {directions = EvenHexDirections;}
    //   else {directions = OddHexDirections;}

    //   // Calculate list of direction


    //   for (int i = 0; i < directions.Length; i++) 
    //   {
    //     Vector2Int adjIndex = new Vector2Int(x + directions[i].x, y + directions[i].y);
    //     bool validIndex = (adjIndex.x >= 0 && adjIndex.x < xDim && adjIndex.y >= 0 && adjIndex.y < yDim);

    //     if (validIndex) {
    //       adjacentTiles.Add(adjIndex);
    //     }
    //   }

    //   return adjacentTiles;
    // }


    // public static List<Tile> TilesInRange(Vector2Int pos, int dist, World worldMap) {
    //   List<Tile> tilesInRange = new List<Tile>();
    //   Vector2Int[] tileVecs;

    //   // Calculate list of tile directions for given range
    //   // If row is even, Shift Right
    //   // For a given distance, the resultant hexagon of found tiles
    //   // will have rows equal to twice the distance + 1 (for the row of the tile at pos)
    //   // hexRows is ALWAYS odd - handy
    //   // so posRow is |dist-row|
    //   int hexRows = dist * 2 + 1;
    //   bool tileRosIsEven = pos.x % 2 == 0; // Is the tile @ pos on an even row
    //   for (int row = 0; row < hexRows; i++) // for each row
    //   {
    //     for (int tile = 0; x`)
    //     // If posRow is even, then first row looked at will get shifted,
    //     // else second one will be. Alternates
    //     Vector2Int direction; //(x, row-dist)
    //     int offset = Math.Abs(dist - row);

    //     // UP is negative. IE 0,0 is is top left corner. Directions in the 'up' direction have negative y


    //   }

    //   return tilesInRange;
    // } 


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
