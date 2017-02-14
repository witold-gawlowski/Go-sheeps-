using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapPosition : MonoBehaviour
{
  public static Vector3 WrapDifference(Vector3 a, Vector3 b)
  {
    float xDiff = a.x - b.x;
    float altXDiff = xDiff + (xDiff < 0 ? Arena.Width : -Arena.Width);
    float zDiff = a.z - b.z;
    float altZDiff = zDiff + (zDiff < 0 ? Arena.Height : -Arena.Height);
    return new Vector3(Mathf.Abs(xDiff) < Mathf.Abs(altXDiff) ? xDiff : altXDiff,
      a.y-b.y,
      Mathf.Abs(zDiff) < Mathf.Abs(altZDiff) ? zDiff : altZDiff
      );
  }

	void Awake()
  {
		
	}
	
	void Update ()
    {
        Vector3 position = transform.position;

        if (position.x < Arena.Width * -0.5f)
        {
            position.x += Arena.Width;
        }
        else if (position.x > Arena.Width * 0.5f)
        {
            position.x -= Arena.Width;
        }

        if (position.z < Arena.Height * -0.5f)
        {
            position.z += Arena.Height;
        }
        else if (position.z > Arena.Height * 0.5f)
        {
            position.z -= Arena.Height;
        }

        transform.position = position;
    }
}
