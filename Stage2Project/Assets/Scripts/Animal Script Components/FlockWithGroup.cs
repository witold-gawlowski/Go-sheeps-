using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class FlockWithGroup : MonoBehaviour
{
  [SerializeField]
  private float BuddyDistance = 100.0f;

  [SerializeField]
  private float AvoidDistance = 1.0f;

  [SerializeField]
  private float CheckForBuddiesInterval = 10.0f;

  [SerializeField]
  private float cohesionCoefficient = 0.1f;

  [SerializeField]
  private float avoidCoefficient = 0.07f;

  [SerializeField]
  private float allignCoefficient = 0.4f;

  [SerializeField]
  private float separationCoefficient = 200.0f;

  [SerializeField]
  private float forwardDrive = 80.0f;

  [SerializeField]
  private float maxForceMag = 1000.0f;
  [SerializeField]
  private bool cohesionForceProportionalToDistance = true;

  private List<GroupTag> mCurrentBuddies;
  private Rigidbody mBody;
  private float mCountDownToCheck;
  private GroupTag groupTag;
  private int cohesionCount;
  public bool isOnGrass;


  void Awake()
  {
    mCurrentBuddies = new List<GroupTag>();
    mBody = GetComponent<Rigidbody>();
    mCountDownToCheck = 0.0f;
    groupTag = GetComponent<GroupTag>();
    isOnGrass = false;
  }

  void Update()
  {
    mCountDownToCheck -= Time.deltaTime;
    if (mCountDownToCheck <= 0.0f)
    {
      UpdateBuddyList();
      mCountDownToCheck = CheckForBuddiesInterval;
    }

    FlockWithBuddies();
    if (!isOnGrass)
    {
      mBody.AddForce(transform.forward * forwardDrive);
    }
  }

  public void EnterGrass()
  {
    isOnGrass = true;
  }

  public void ExitGrass()
  {
    isOnGrass = false;
  }

  public int GetColorBuddyCount()
  {
    return cohesionCount;
  }

  public int GetBuddyCount()
  {
    return mCurrentBuddies.Count;
  }

  private void UpdateBuddyList()
  {
    GroupTag[] individuals = FindObjectsOfType<GroupTag>();

    for (int count = 0; count < individuals.Length; ++count)
    {
      if (individuals[count].gameObject != gameObject && ! (individuals[count].tag == "Player"  && tag == "Sheep"))
      {
        Vector3 difference = WrapPosition.WrapDifference(individuals[count].transform.position, transform.position);
        if (difference.magnitude <= BuddyDistance)
        {
          if (!mCurrentBuddies.Contains(individuals[count]))
          {
            mCurrentBuddies.Add(individuals[count]);
          }
        }
        else if (mCurrentBuddies.Contains(individuals[count]))
        {
          mCurrentBuddies.Remove(individuals[count]);
        }
      }
    }
    mCurrentBuddies = mCurrentBuddies.Where(x => x != null).ToList();
  }

  private void FlockWithBuddies()
  {
    cohesionCount = 0;
    if (mCurrentBuddies.Count > 0)
    {
      Vector3 align = Vector3.zero;
      Vector3 avoid = Vector3.zero;
      Vector3 cohesion = Vector3.zero;
      Vector3 separation = Vector3.zero;

      for (int count = 0; count < mCurrentBuddies.Count; ++count)
      {
        if (mCurrentBuddies[count] == null)
        {
          break;
        }
        Rigidbody body = mCurrentBuddies[count].GetComponent<Rigidbody>();
        Vector3 buddyToThis = WrapPosition.WrapDifference(mCurrentBuddies[count].transform.position,
          transform.position);
        float mag = buddyToThis.magnitude;
        if (mCurrentBuddies[count].GetComponent<GroupTag>().Affiliation.Equals(groupTag.Affiliation))
        {
          align += body.velocity;
          cohesion += buddyToThis;
          cohesionCount++;
          if (mag < AvoidDistance)
          {
            avoid -= buddyToThis.normalized * (1 / (mag + 0.1f));
          }
        }
        else
        {
          separation -= buddyToThis.normalized * (1 / (mag + 0.1f));
        }
      }

      align = align/mCurrentBuddies.Count - mBody.velocity;

      if (cohesionCount != 0)
      {
        cohesion /= cohesionCount;
      }
      if (!cohesionForceProportionalToDistance)
      {
        cohesion = cohesion.normalized;
      }
      Vector3 force = (align * allignCoefficient +
                       cohesion * cohesionCoefficient +
                       avoid * avoidCoefficient +
                       separation * separationCoefficient
      );
      force = Vector3.ClampMagnitude(force, maxForceMag);
      mBody.AddForce( force * Time.deltaTime);
    }
  }
}
