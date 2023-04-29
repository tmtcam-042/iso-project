using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TileType 
{ 
  public string Name { get; set; }
  public float Weight { get; set; }
  public Color Color { get; set; }
  public TileRule[] Rules { get; set; }

  public TileType(string name, float weight, Color color, TileRule[] rules)
  {
    Name = name;
    Weight = weight;
    Color = color;
    Rules = rules;
  }
}

public abstract class TileRule
{
    public abstract string name { get; set; }
    public abstract void Evaluate();
    public override string ToString()
    {
      return "RULE - " + name;
    }
}

public static class TileTypes {
  public static readonly TileType Neutral = new TileType("Neutral", 1f, Color.white, null);
  public static readonly TileType Forest = new TileType("Forest", 1f, Color.green, null);
  public static readonly TileType River = new TileType("River", 1f, Color.blue, null);
  public static readonly TileType Beach = new TileType("Beach", 1f, new Color(1f, 0.92f, 0.78f), null);
  public static readonly TileType Path = new TileType("Path", 1f, Color.grey, null);
  public static readonly TileType Monument = new TileType("Monument", 1f, Color.black, null);

  public static readonly List<TileType> TileSet = new List<TileType>(){Forest, River, Beach, Path, Monument};
}

public class Tile : MonoBehaviour
{
  public string name = "Tile";
  public bool resolved = false;
  public int q { get; set; }
  public int r { get; set; }
  public double shannonEntropy;
  public TileType Type { 
    get
    {
      return _type;
    }
    set
    {
      _type = value;
      name = Type.Name;
      SetTileColor(Type.Color);
    } 
  }
  public List<TileType> tileSet = TileTypes.TileSet;

  // Private backing field to prevent stack overflow
  private TileType _type;

  private Renderer renderer;

  public void Awake()
  {
    renderer = GetComponentInChildren<Renderer>();
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

  public override string ToString() {
    return name + " at (" + q + ", " + r + ") has positions (" + transform.position.x + ", " + transform.position.y + ").";
  }
}
