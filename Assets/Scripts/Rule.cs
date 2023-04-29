using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Adjacency : TileRule
{
  new public string name = "Adjacency";
  private List<TileType> permittedTiles = new List<TileType>();


  public Adjacency(List<TileType> validTileSet)
  {
    // Making sure to use the same
    permittedTiles = validTileSet;
  }

  public override void Evaluate(Tile target, World world)
  {
    List<GameObject> adjacentHexes = world.AdjacentHexes(target);

    foreach(GameObject adjHex in adjacentHexes)
    {
      Tile adjTile = adjHex.GetComponent<Tile>();
      if (adjTile.resolved == true) { continue; }

      List<TileType> tileTypesToRemove = new List<TileType>();
      foreach(TileType tileType in adjTile.tileSet)
      {
        // If the tile type is not on the permitted list of adjacent tiles, remove it
        if(!permittedTiles.Contains(tileType))
        {
          tileTypesToRemove.Add(tileType);
        }
      }

      foreach(TileType tileType in tileTypesToRemove)
      {
        adjTile.tileSet.Remove(tileType);
      }

      adjTile.CalculateShannonEntropy();
    }
  }
}

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

/*
* All rules should have access to the same information, sent down to them from
* the tile object
*/
public abstract class TileRule
{
    public string name;
    public World world;
    public abstract void Evaluate(Tile target, World world);
    public override string ToString()
    {
      return "RULE - " + name;
    }
}