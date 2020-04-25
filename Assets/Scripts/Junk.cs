using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      foreach (Transform children in transform) {
          int roundedX = Mathf.RoundToInt(children.transform.position.x);
          int roundedY = Mathf.RoundToInt(children.transform.position.y);

          TetrisBlock.grid[roundedX, roundedY] = children;
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
