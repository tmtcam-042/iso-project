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