using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
  public void OnTriggerEnter(Collider collider)
  {
    FurManager furManager = collider.GetComponent<FurManager>();
    if (furManager)
    {
      furManager.Shave();
    }
  }
}
