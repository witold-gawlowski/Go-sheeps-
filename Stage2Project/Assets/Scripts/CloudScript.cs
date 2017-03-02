using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {
  [SerializeField]
  private Animator cloudAnimator;
  
  [SerializeField]
  private float cloudMinBreakLength = 15;

  [SerializeField]
  private float cloudMaxBreakLength = 35;


  private float counter;

  void Start()
  {
    counter = Random.Range(cloudMinBreakLength, cloudMaxBreakLength); ;
  }

  void Update()
  {
    counter -= Time.deltaTime;
    if(counter < 0)
    {
      counter = Random.Range(cloudMinBreakLength, cloudMaxBreakLength);
      cloudAnimator.SetTrigger("TriggerCloud");
    }
  }
}
