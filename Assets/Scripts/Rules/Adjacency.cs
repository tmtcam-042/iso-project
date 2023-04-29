using System.Collections;
using System.Collections.Generic;
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