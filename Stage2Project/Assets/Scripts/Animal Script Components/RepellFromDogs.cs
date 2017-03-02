using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RepellFromDogs : MonoBehaviour
{
  public enum Type { Attract, Repel }

  [SerializeField]
  private float ForceCoefficient = 1000.0f;

  [SerializeField]
  private float forceCap = 1000.0f;

  [SerializeField]
  private float MinimumDistance = 1.0f;

  [SerializeField]
  private Type MagnetizeType = Type.Repel;

  private static List<GameObject> mPlayers;
  private Rigidbody mBody;

  void Start()
  {
    mPlayers = GameObject.FindGameObjectsWithTag("Player").ToList();
    mBody = GetComponent<Rigidbody>();
  }

  public static void RegisterShepard(GameObject shepard)
  {
    mPlayers.Add(shepard);
  }

  void Update()
  {
    for(int i=0; i < mPlayers.Count; i++)
    {
      Vector3 playerToBoid = WrapPosition.WrapDifference(transform.position, mPlayers[i].transform.position);
      float mag = playerToBoid.magnitude;
      if (mag <= MinimumDistance)
      {
        if (MagnetizeType == Type.Repel)
        {
          float forceMagnitude =  1 / mag / mag * ForceCoefficient ;
          forceMagnitude = Mathf.Min(forceMagnitude, forceCap);
          mBody.AddForce(playerToBoid.normalized * forceMagnitude * Time.deltaTime);
        }
        else
        {
          mBody.AddForce(-playerToBoid.normalized * ForceCoefficient * Time.deltaTime);
        }
       
      }
    }
  }
}
