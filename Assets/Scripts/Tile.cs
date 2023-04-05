using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public int x;
  public int y;
  
  /*
  Calculate world position of hex based on x and y coord. 0, 0 is at origin
  Returns vector3 of object position
  */
  private Vector3 CalculatePosition() {
    Vector3 position = new Vector3(x, y, 0);
    return position;
  }


  public override string ToString() {
    return "Tile at (" + x + ", " + y + ") has positions (" + transform.position.x + ", " + transform.position.y + ").";
  }
}
