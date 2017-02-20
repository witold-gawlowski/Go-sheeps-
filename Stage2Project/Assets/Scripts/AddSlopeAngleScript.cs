using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSlopeAngleScript : MonoBehaviour
{
  [SerializeField]
  private Vector3 upDirection = new Vector3(0, 1, 1);
  [SerializeField]
  private Vector3 slopeNormal = new Vector3(0, 1, -1);
  public float slopeAngle = 45.0f;
  public float sheepSlopeVsForwardAngle;
  void Update()
  {
    //transform.rotation = Quaternion.AngleAxis(slopeAngle, transform.forward);
    //transform.up = Vector3.ProjectOnPlane(transform.up, upDirection);
    sheepSlopeVsForwardAngle = Vector3.Angle(Vector3.forward,
      Vector3.ProjectOnPlane(transform.parent.forward, Vector3.up));
    Vector3 cross = Vector3.Cross(Vector3.forward, Vector3.ProjectOnPlane(transform.parent.forward, Vector3.up));
    if (cross.y < 0)
    {
      sheepSlopeVsForwardAngle *= -1;
    }
    transform.localEulerAngles = new Vector3(0, 0, slopeAngle * Mathf.Sin(sheepSlopeVsForwardAngle*Mathf.PI/180));
  }
}
