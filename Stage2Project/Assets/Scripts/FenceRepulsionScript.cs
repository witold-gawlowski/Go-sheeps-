using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceRepulsionScript : MonoBehaviour {

  //To reduce bouncyness I consider increasing drag in trigger enter and reducing it on trigger exit. 

  [SerializeField]
  private float repulsionCoefficient = 30000.0f;
  [SerializeField]
  private float forceCap = 800.0f;


  void OnTriggerStay(Collider other)
  {
    if (other.tag != "Sheep")
    {
      return;
    }
    float perpendicularDistance = Vector3.Dot(other.transform.position - transform.position, transform.right);
    //Debug.DrawRay(other.transform.position, transform.right / perpendicularDistance);
    Vector3 force = transform.right / (Mathf.Abs(perpendicularDistance) * perpendicularDistance) * Time.deltaTime * repulsionCoefficient;
    force = Vector3.ClampMagnitude(force, forceCap);
    //todo: register colliding bodies in OnTriggerEnter and Exit in odrder to reduce GetComponent calls. 
    other.GetComponentInParent<Rigidbody>().AddForce(force);
  }
}
