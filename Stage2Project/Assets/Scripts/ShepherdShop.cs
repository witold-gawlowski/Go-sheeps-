using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepherdShop : MonoBehaviour
{
  [SerializeField]
  private GameObject ui;
  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      Player player = other.GetComponent<Player>();
      if (player)
      {
        ui.SetActive(true);
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      Player player = other.GetComponent<Player>();
      if (player)
      {
        ui.SetActive(false);
      }
    }
  }
}
