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
}
