using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utils
{
  // Method used for hexTiles indexing keys
  // R = x, Q = y
  public static string StringCoords(int r, int q)
  {
    return $"[{r},{q}]";
  }

  public static Vector2Int AxialAdd(Tile tile, Vector2Int vec)
  {
    return new Vector2Int(tile.r + vec.x, tile.q + vec.y);
  }

  public static void ResetTileColour(GameObject tileObject)
  {
    Tile tile = tileObject.GetComponent<Tile>();
    Color baseColor = tile.Type.Color;
    tile.SetTileColor(baseColor);
  }
}
