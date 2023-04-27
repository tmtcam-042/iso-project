using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TileType 
{ 
  public string Name { get; set; }
  public Color Color { get; set; }
  public TileRule[] Rules { get; set; }

  public TileType(string name, Color color, TileRule[] rules)
  {
    Name = name;
    Color = color;
    Rules = rules;
  }
}

public class TileRule
{

}

public static class TileTypes {
  public static readonly TileType Neutral = new TileType("Neutral", Color.white, null);
  public static readonly TileType Forest = new TileType("Forest", Color.green, null);
  public static readonly TileType Beach = new TileType("Beach", new Color(1f, 0.92f, 0.78f), null);
  public static readonly TileType Path = new TileType("Path", Color.white, null);
  public static readonly TileType Monument = new TileType("Monument", Color.black, null);

}

public class Tile : MonoBehaviour
{
  public string name = "Tile";
  public int q { get; set; }
  public int r { get; set; }
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


  public override string ToString() {
    return name + " at (" + q + ", " + r + ") has positions (" + transform.position.x + ", " + transform.position.y + ").";
  }
}
