using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {
  public Animator cloudAnimator;
  float counter;
  void Start()
  {
    counter = Random.Range(20, 30); ;
  }

  void Update()
  {
    counter -= Time.deltaTime;
    if(counter < 0)
    {
      counter = Random.Range(20, 30);
      cloudAnimator.SetTrigger("TriggerCloud");
    }
  }
}
