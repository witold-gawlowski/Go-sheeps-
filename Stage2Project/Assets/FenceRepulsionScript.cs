using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceRepulsionScript : MonoBehaviour {

  //To reduce bouncyness I can consider increasing drag in trigger enter and reducing it on trigger exit. 

  void OnTriggerStay(Collider other)
  {
    if (other.tag != "Sheep")
    {
      return;
    }
    float perpendicularDistance = Vector3.Dot(other.transform.position - transform.position, transform.right)+0.1f;
    Debug.DrawRay(other.transform.position, transform.right / perpendicularDistance);
    Vector3 force = transform.right / (Mathf.Abs(perpendicularDistance) * perpendicularDistance) * Time.deltaTime * 30000;
    force = Vector3.ClampMagnitude(force, 1000.0f);
    other.GetComponentInParent<Rigidbody>().AddForce(force);
  }
}
