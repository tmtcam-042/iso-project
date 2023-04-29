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
        //Debug.Log("Adjudicating if tile type " + tileType + " is permitted for " + adjTile);
        // If the tile type is not on the permitted list of adjacent tiles, remove it
        if(!permittedTiles.Contains(tileType))
        {
          tileTypesToRemove.Add(tileType);
          //Debug.Log(name + " rule evaluated, " + tileType + " removed from " + adjTile);
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