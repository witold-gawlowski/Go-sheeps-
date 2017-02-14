using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPlayerDistance : MonoBehaviour
{
  private GameObject player;

  void Start()
  {
    player = GameObject.FindWithTag("Player");
  }

  public void Update()
  {
    Debug.DrawLine(transform.position, transform.position + WrapPosition.WrapDifference(player.transform.position, transform.position));
  }

}
