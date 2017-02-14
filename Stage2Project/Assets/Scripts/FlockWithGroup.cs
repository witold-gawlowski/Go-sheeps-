using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlockWithGroup : MonoBehaviour
{
  [SerializeField]
  private GroupTag.Group GroupCode;

  [SerializeField]
  private float Speed;

  [SerializeField]
  private float BuddyDistance = 100.0f;

  [SerializeField]
  private float AvoidDistance = 1.0f;

  [SerializeField]
  private float CheckForBuddiesInterval = 10.0f;

  [SerializeField] private float cohesionCoefficient = 0.1f;

  private List<GroupTag> mCurrentBuddies;
  private Rigidbody mBody;
  private float mCountDownToCheck;

  void Awake()
  {
    mCurrentBuddies = new List<GroupTag>();
    mBody = GetComponent<Rigidbody>();
    mCountDownToCheck = 0.0f;
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
  }

  private void UpdateBuddyList()
  {
    GroupTag[] individuals = FindObjectsOfType<GroupTag>();

    for (int count = 0; count < individuals.Length; ++count)
    {
      if (individuals[count].gameObject != gameObject && individuals[count].Affiliation == GroupCode)
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
    //for (int count = 0; count < mCurrentBuddies.Count; ++count)
    //{
    //  Debug.DrawLine(transform.position, mCurrentBuddies[count].transform.position, Color.cyan, CheckForBuddiesInterval);
    //}
  }

  private void FlockWithBuddies()
  {
    if (mCurrentBuddies.Count > 0)
    {
      Vector3 align = Vector3.zero;
      Vector3 cohesion = Vector3.zero;
      Vector3 avoid = Vector3.zero;
      Vector3 myCohesion = Vector3.zero;

      for (int count = 0; count < mCurrentBuddies.Count; ++count)
      {
        Rigidbody body = mCurrentBuddies[count].GetComponent<Rigidbody>();
        align += body.velocity;
        cohesion += mCurrentBuddies[count].transform.position;
        myCohesion += WrapPosition.WrapDifference(mCurrentBuddies[count].transform.position,
          transform.position);
        
        if ((mCurrentBuddies[count].transform.position - transform.position).magnitude < AvoidDistance)
        {
          avoid += mCurrentBuddies[count].transform.position;
        }
      }

      /* Align now is an average velocity of a group.
      *  we should be adding the difference beteween the average velocity and our velocity instead of 
      *  average velocity.
      */

      align /= mCurrentBuddies.Count;
      cohesion /= mCurrentBuddies.Count;
      avoid /= mCurrentBuddies.Count;
      myCohesion /= mCurrentBuddies.Count;

      align.Normalize();
      cohesion = cohesion - transform.position;
      cohesion.Normalize();
      avoid = transform.position - avoid;
      avoid.Normalize();

      mBody.AddForce((align + myCohesion * cohesionCoefficient + avoid) * Speed * Time.deltaTime);
    }
  }
}
