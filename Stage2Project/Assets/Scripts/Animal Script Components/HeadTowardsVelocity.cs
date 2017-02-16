using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTowardsVelocity : MonoBehaviour
{
  [SerializeField]
  private float turnSpeed = 1.0f;

  [SerializeField]
  private float minimumVelocity = 1.0f;



  private Rigidbody mBody;

  void Start()
  {
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    if (mBody.velocity.magnitude > minimumVelocity)
    {
      transform.forward += mBody.velocity.normalized * turnSpeed;//* Mathf.Pow(mBody.velocity.magnitude, 1);
    }
  }
}
