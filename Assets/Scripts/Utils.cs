using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utils
{
  // Method used for hexTiles indexing keys
  public static string StringCoords(int r, int q)
  {
    return $"[{r},{q}]";
  }

  public static Vector2Int AxialAdd(Tile tile, Vector2Int vec)
  {
    return new Vector2Int(tile.r + vec.x, tile.q + vec.y);
  }
}
