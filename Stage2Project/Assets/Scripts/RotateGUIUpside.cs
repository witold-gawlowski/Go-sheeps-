using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGUIUpside : MonoBehaviour {
  void Start()
  {
    Camera camera = GameObject.FindGameObjectWithTag("LevelPrefab").GetComponentInChildren<Camera>();
    transform.transform.up = camera.transform.up;
  }
}
