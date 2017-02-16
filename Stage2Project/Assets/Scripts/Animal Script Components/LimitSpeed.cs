using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitSpeed : MonoBehaviour
{
  [SerializeField]
  private float MaxSpeed = 10.0f;

  [SerializeField] private float speedLimitingFactor = 100.0f;

  private Rigidbody mBody;


  void Start()
  {
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    float excess = mBody.velocity.magnitude - MaxSpeed;
    if (excess > 0)
    {
      mBody.AddForce(-mBody.velocity.normalized*excess*speedLimitingFactor);
    }
  }
}
