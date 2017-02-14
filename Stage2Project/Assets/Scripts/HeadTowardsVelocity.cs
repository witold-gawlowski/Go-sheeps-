using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTowardsVelocity : MonoBehaviour
{
  [SerializeField] private float torqueMultiplier = 30.0f;
  private Rigidbody mBody;

  void Start()
  {
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    if (mBody.velocity.magnitude > 0.4f)
    {
      mBody.AddTorque(Vector3.Cross(transform.forward, mBody.velocity)*torqueMultiplier);
    }
  }
}
