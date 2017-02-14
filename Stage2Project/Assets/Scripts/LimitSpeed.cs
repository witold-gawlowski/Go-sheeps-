using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitSpeed : MonoBehaviour
{
  [SerializeField]
  private float MaxSpeed = 10.0f;
  private Rigidbody mBody;

  void Start()
  {
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    if (mBody.velocity.magnitude > MaxSpeed)
    {
      mBody.AddForce(-mBody.velocity);
    }
  }
}
