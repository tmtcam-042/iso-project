using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public int q {get; set;}
  public int r {get; set;}

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
    return "Tile at (" + q + ", " + r + ") has positions (" + transform.position.x + ", " + transform.position.y + ").";
  }
}
