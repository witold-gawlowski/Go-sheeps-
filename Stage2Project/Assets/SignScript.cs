using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour {
  [SerializeField]
  private GameObject ui;


  void Start()
  {
    //.GetComponentInChildren<>
  }
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      ui.SetActive(true);
    }
  }
  void OnTriggerExit(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      ui.SetActive(false);
    }
  }
}
