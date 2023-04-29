using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public string name = "Tile";
  public bool resolved = false;
  public int q { get; set; }
  public int r { get; set; }

  public double shannonEntropy;
  public World world;

  public TileType Type { 
    get
    {
      return _type;
    }
    set
    {
      _type = value;
      name = value.Name;
      SetTileColor(value.Color);
      value.EvaluateRules(this, world);
      if (value.Name != "Neutral")
      {
        resolved = true;
      }
    } 
  }
  public List<TileType> tileSet;
  public int count;

  // Private backing field to prevent stack overflow
  private TileType _type;

  private Renderer renderer;

  public void Awake()
  {
    renderer = GetComponentInChildren<Renderer>();
    tileSet = new List<TileType>(TileTypes.Instance.TileSet); // Create a copy rather than a reference
  }

  public void SetTileColor(Color setColor) 
  {
    renderer.material.color = setColor;
  }
  
  // Calculates and updates the current tile's Shannon Entropy.
  public void CalculateShannonEntropy() 
  {
    double sumWeight = 0f;
    double logSumWeight = 0f;
    foreach (TileType tile in tileSet)
    {
      sumWeight += tile.Weight;
      logSumWeight += tile.Weight * Math.Log(tile.Weight);
    }
    
    shannonEntropy = Math.Log(sumWeight) - (logSumWeight / sumWeight);
  }

  public TileType PickRandomTypeFromTile()
  {
    if (tileSet.Count == 0)
    {
      Debug.LogWarning(ToString() + " does not contain any TileType in the given tileSet");
      return TileTypes.Instance.Error;
    }

    // Calculate the sum of all weights
    float totalWeight = 0f;
    foreach(TileType tileType in tileSet)
    {
      totalWeight += tileType.Weight;
    }

    // Generate a random number between 0 and the sum of weights
    float randomNumber = UnityEngine.Random.Range(0f, totalWeight);

    // Select the tile corresponding to the random number generated
    float weightSum = 0f;
    foreach (TileType tileType in tileSet)
    {
      weightSum += tileType.Weight;
      if (randomNumber <= weightSum)
      {
        return tileType;
      }
    }

    // Error catching
    return TileTypes.Instance.Error;
  }

  public override string ToString() {
    return name + " at (" + r + ", " + q + ")";
  }
}
