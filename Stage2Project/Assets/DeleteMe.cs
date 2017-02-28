using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe : MonoBehaviour {
  public void ClearPlayerPrefs()
  {
    PlayerPrefs.DeleteAll();
  }
}
