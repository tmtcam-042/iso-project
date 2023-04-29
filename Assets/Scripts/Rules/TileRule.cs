using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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