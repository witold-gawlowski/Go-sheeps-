using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MagnetizedByPlayer : MonoBehaviour
{
  public enum Type { Attract, Repel }

  [SerializeField]
  private float RepelForce = 1000.0f;

  [SerializeField]
  private float MinimumDistance = 1.0f;

  [SerializeField]
  private Type MagnetizeType = Type.Repel;

  private Player[] mPlayers;
  private Rigidbody mBody;

  void Awake()
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
      if (playerToBoid.magnitude <= MinimumDistance)
      {
        mBody.AddForce((MagnetizeType == Type.Repel ? 1 : -1)*playerToBoid.normalized *1/mag* RepelForce * Time.deltaTime);
      }
    }
  }
}
