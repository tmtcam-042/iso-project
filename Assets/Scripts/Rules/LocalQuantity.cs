using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalQuantity: TileRule
{
  new public string name = "Local Quantity";
  private TileType checkTile;
  private int distance;
  private int quantity;

  public LocalQuantity(TileType tile, int _distance, int _quantity)
  {
    checkTile = tile;
    distance = _distance;
    quantity = _quantity;
  }
  
  public override void Evaluate(Tile target, World world)
  {
    Debug.Log("Evaluating local quantity of " + checkTile.Name + " in range of " + target);
    
    // Grab all tiles within range
    List<GameObject> localHexes = world.TilesInRange(target, distance, false);
    int count = 1; // Count of how many of given tiletype are within range
    foreach(GameObject localHex in localHexes)
    {
      TileType tileType = localHex.GetComponent<Tile>().Type;
      if (tileType.Name == checkTile.Name)
      {
        count += 1;
        Debug.Log("Monument found, count up to: " + count);
      }
    }

    // If count has exceeded quantity, forbid checkTiles from appearing within range
    if (count >= quantity)
    {
      foreach(GameObject localHex in localHexes)
      {
        Tile localTile = localHex.GetComponent<Tile>();
        localTile.tileSet.Remove(checkTile);
        Debug.Log("Removed monument from tiles around " + checkTile);
      }
    }
  }
}