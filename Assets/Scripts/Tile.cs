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

  public void EvaluateRules(Tile target, World world)
  {
    // Skip if no rules
    if (Rules == null)
    {
      return;
    }

    foreach(TileRule rule in Rules)
    {
      rule.Evaluate(target, world);
    }

    target.CalculateShannonEntropy();
  }
  
  public override bool Equals(object obj)
  {
    if (obj == null || GetType() != obj.GetType())
    {
      return false;
    }

    TileType other = (TileType)obj;
    return this.Name == other.Name; // Assuming 'name' is unique for each TileType
  }

  public override int GetHashCode()
  {
    return this.Name.GetHashCode();
  }

  public override string ToString()
  {
    return Name;
  }

}

public class TileTypes {

  // Singleton instance
  private static TileTypes _instance;
  public static TileTypes Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = new TileTypes();
      }
      return _instance;
    }
  }

  public TileType Neutral;
  public TileType Forest;
  public TileType River;
  public TileType Beach;
  public TileType Path;
  //public TileType Monument;

  public TileType Error;

  public List<TileType> TileSet;

  private TileTypes() // Initialise all TileTypes rules etc in here
  { 
    Forest = new TileType("Forest", 1f, Color.green, null);
    River = new TileType("River", 1f, Color.blue, null);
    Beach = new TileType("Beach", 1f, new Color(1f, 0.92f, 0.78f), null);
    Path = new TileType("Path", 1f, Color.grey, null);
    Neutral = new TileType("Neutral", 1f, Color.white, null);

    Error = new TileType("Error", 1f, Color.red, null);

    TileSet = new List<TileType>() { Forest, River, Beach, Path };

    DefineAdjacencyRules();
  }

  private void DefineAdjacencyRules()
  {
    Forest.Rules = new TileRule[] { new Adjacency(new List<TileType> { Beach, Forest, Path }) };
    River.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, Beach }) };
    Beach.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, Forest }) };
    Path.Rules = new TileRule[] { new Adjacency(new List<TileType> { Forest, Path }) };
  }
}


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
    if (!resolved)
    {
    SetTileColor(new Color((float)shannonEntropy, (float)shannonEntropy, (float)shannonEntropy));
    }
  }

  public TileType PickRandomTypeFromTile()
  {
    if (tileSet.Count == 0)
    {
      Debug.LogWarning(ToString() + " does not contain any TileType in the given tileSet");
      return TileTypes.Instance.Error;
    }

    // Select a random type from the tile's set
    TileType randomType = tileSet[UnityEngine.Random.Range(0, tileSet.Count)];

    return randomType;
  }

  public override string ToString() {
    return name + " at (" + r + ", " + q + ")";
  }
}
