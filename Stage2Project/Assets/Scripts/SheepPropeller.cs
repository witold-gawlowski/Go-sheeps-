using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPropeller : MonoBehaviour
{
  [SerializeField] private float PropelStrength = 300.0f;
  public void OnTriggerStay(Collider other)
  {
    if (other.tag.Equals("Sheep"))
    {
      other.GetComponentInParent<Rigidbody>().AddForce(transform.forward * PropelStrength);
    }
  }
}
