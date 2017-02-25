using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceRepulsionScript : MonoBehaviour {
  void OnTriggerStay(Collider other)
  {
    float direction = Vector3.Dot(other.transform.position - transform.position, transform.right);
    Debug.DrawRay(other.transform.position, transform.right / direction);
    other.GetComponentInParent<Rigidbody>().AddForce(transform.right / (Mathf.Abs(direction)*direction) * Time.deltaTime*30000);
  }
}
