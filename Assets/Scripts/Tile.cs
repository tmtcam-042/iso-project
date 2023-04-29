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

  public TileType(string name, float weight, Color color)
  {
    Name = name;
    Weight = weight;
    Color = color;
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
  public TileType DeepRiver;
  public TileType Monument;

  public TileType Error;

  public List<TileType> TileSet;

  private TileTypes() // Initialise all TileTypes rules etc in here
  { 
    Forest = new TileType("Forest", 0.6f, new Color(0.137f, 0.545f, 0.137f));
    River = new TileType("River", 0.1f, new Color(0.678f, 0.847f, 0.902f));
    Beach = new TileType("Beach", 0.5f, new Color(1f, 0.92f, 0.78f));
    Path = new TileType("Path", 0.1f, Color.grey);
    DeepRiver = new TileType("DeepRiver", 0.1f, new Color(0f, 0, 0.545f));
    Monument = new TileType("Monument", 10f, Color.black);
    Neutral = new TileType("Neutral", 1f, Color.white);

    Error = new TileType("Error", 1f, Color.red);

    TileSet = new List<TileType>() { Forest, River, DeepRiver, Beach, Path, Monument };

    DefineAdjacencyRules();
  }

  private void DefineAdjacencyRules()
  {
    Forest.Rules = new TileRule[] { new Adjacency(new List<TileType> { Beach, Forest, Path, Monument }) };
    Monument.Rules = new TileRule[] { new Adjacency(new List<TileType> { Forest }), new LocalQuantity(Monument, 10, 1)};
    River.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, Beach, Forest }) };
    Beach.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, Forest, Beach }) };
    Path.Rules = new TileRule[] { new Adjacency(new List<TileType> { Forest, Path }) };
    DeepRiver.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, DeepRiver }) };
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
