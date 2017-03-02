using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScript : MonoBehaviour
{
  [SerializeField]
  private float grassAttractionFroce = 1000.0f;
  public void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Sheep")
    {
      other.gameObject.SendMessageUpwards("EnterGrass");
    }
  }

  public void OnTriggerStay(Collider other)
  {
    if (other.tag == "Sheep")
    {
      //todo: refactor so that getcomponent is not called every frame
      Rigidbody otherRigidbody = other.GetComponentInParent<Rigidbody>();
      Vector3 sheepToGrass = other.transform.position - transform.position;
      otherRigidbody.AddForce(-sheepToGrass * grassAttractionFroce);
    }
  }

  public void OnTriggerExit(Collider other)
  {
    if (other.tag == "Sheep")
    {
      other.gameObject.SendMessageUpwards("ExitGrass");
    }
  }
}
