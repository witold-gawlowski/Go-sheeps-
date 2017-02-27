using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldGUI : MonoBehaviour {
  void Start()
  {
    transform.transform.up = Camera.main.transform.up;
  }
}
