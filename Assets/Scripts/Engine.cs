using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField]
    private float timeStep = 0.5f;
    [SerializeField]
    private World map;


    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = timeStep;
        /*
        * Loop over whole map. For each already defined tile, 
        * run Evaluation on it and reduce surrounding tile's probabilities
        * Calculate Shannon Entropy of whole map.
        * Begin!
        */

        // Loop over whole map - first pass
        foreach(var (pos, hexTile) in map.hexTiles)
        {
          if (hexTile.resolved)
          {
            /*
            * Evaluate defined tile.
            */
          }
        }

        foreach(var (pos, hexTile) in map.hexTiles)
        {
          hexTile.CalculateShannonEntropy();
        }

        // Done!
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        * Step 1 - check if ready
        * Step 2 - Select tile with lowest shannon entropy
        * Step 3 - Pick an option
        * Step 4 - 
        */
    }
}
