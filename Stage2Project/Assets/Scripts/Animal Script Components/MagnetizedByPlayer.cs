using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MagnetizedByPlayer : MonoBehaviour
{
  public enum Type { Attract, Repel }

  [SerializeField]
  private float ForceCoefficient = 1000.0f;

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
          mBody.AddForce(playerToBoid.normalized / mag/mag * ForceCoefficient * Time.deltaTime);
        }
        else
        {
          mBody.AddForce(-playerToBoid.normalized * ForceCoefficient * Time.deltaTime);
        }
       
      }
    }
  }
}
