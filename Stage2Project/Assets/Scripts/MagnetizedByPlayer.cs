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

  private Player mPlayer;
  private Rigidbody mBody;

  void Awake()
  {
    mPlayer = FindObjectOfType<Player>();
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    if (mPlayer != null)
    {
      Vector3 playerToBoid = WrapPosition.WrapDifference(transform.position, mPlayer.transform.position);
      if (playerToBoid.magnitude <= MinimumDistance)
      {
        mBody.AddForce((MagnetizeType == Type.Repel ? playerToBoid : -playerToBoid) * RepelForce * Time.deltaTime);
      }
    }
  }
}
