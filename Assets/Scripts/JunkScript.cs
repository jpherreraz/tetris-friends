﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkScript : MonoBehaviour
{
  public GameObject Junk;
  public static int numberOfLines = 0;

  void SpawnJunk() {
    Instantiate(Junk, transform.position, Quaternion.identity);
    transform.position = new Vector3(0,numberOfLines,0);
  }
  void Update() {
    if (Input.GetKeyDown(KeyCode.Y)) {
      numberOfLines += 1;
      SpawnJunk();
    }
  }
}
