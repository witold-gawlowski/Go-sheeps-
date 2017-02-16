using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmForceManager : MonoBehaviour
{
  [SerializeField]
  private float farmRepulsionForce = 100.0f;

  [SerializeField]
  private float forceAttenuationParam = 10.0f;

  [SerializeField]
  private Collider entranceCollider;

void OnTriggerStay(Collider other)
  {
    if (other.tag == "Sheep" && ! other.bounds.Intersects(entranceCollider.bounds))
    {
      Vector3 farmToSheep = other.transform.position - transform.position;
      float mag = farmToSheep.magnitude;
      other.GetComponentInParent<Rigidbody>().AddForce(
        farmToSheep.normalized * 1/(mag+ forceAttenuationParam) * farmRepulsionForce
        );
    }
  }
}
