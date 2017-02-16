using System.Collections;
using System.Collections.Generic;
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

  private Player[] mPlayers;
  private Rigidbody mBody;

  void Start()
  {
    mPlayers = FindObjectsOfType<Player>();
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    for(int i=0; i<mPlayers.Length; i++)
    {
      Vector3 playerToBoid = WrapPosition.WrapDifference(transform.position, mPlayers[i].transform.position);
      float mag = playerToBoid.magnitude;
      if (mag <= MinimumDistance)
      {
        if (MagnetizeType == Type.Repel)
        {
          mBody.AddForce(playerToBoid.normalized / mag * ForceCoefficient * Time.deltaTime);
        }
        else
        {
          mBody.AddForce(-playerToBoid.normalized * ForceCoefficient * Time.deltaTime);
        }
       
      }
    }
  }
}
