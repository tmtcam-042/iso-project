using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  //public TileType Monument;

  public TileType Error;

  public List<TileType> TileSet;

  private TileTypes() // Initialise all TileTypes rules etc in here
  { 
    Forest = new TileType("Forest", 0.6f, new Color(0.137f, 0.545f, 0.137f));
    River = new TileType("River", 0.1f, new Color(0.678f, 0.847f, 0.902f));
    Beach = new TileType("Beach", 0.3f, new Color(1f, 0.92f, 0.78f));
    Path = new TileType("Path", 0.8f, Color.grey);
    DeepRiver = new TileType("DeepRiver", 0.4f, new Color(0f, 0, 0.545f));
    //Monument = new TileType("Monument", 10f, Color.black);
    Neutral = new TileType("Neutral", 1f, Color.white);

    Error = new TileType("Error", 1f, Color.red);

    TileSet = new List<TileType>() { Forest, River, DeepRiver, Beach, Path };

    DefineAdjacencyRules();
  }

  private void DefineAdjacencyRules()
  {
    Forest.Rules = new TileRule[] { new Adjacency(new List<TileType> { Beach, Forest, Path }) };
    //Monument.Rules = new TileRule[] { new Adjacency(new List<TileType> { Forest }), new LocalQuantity(Monument, 10, 1)};
    River.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, Beach }) };
    Beach.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, Forest, Beach }) };
    Path.Rules = new TileRule[] { new Adjacency(new List<TileType> { Forest, Path }), new LocalQuantity(Path, 1, 3) };
    DeepRiver.Rules = new TileRule[] { new Adjacency(new List<TileType> { River, DeepRiver }) };
  }
}