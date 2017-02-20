using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlockWithGroup : MonoBehaviour
{
  [SerializeField]
  private float BuddyDistance = 100.0f;

  [SerializeField]
  private float AvoidDistance = 1.0f;

  [SerializeField]
  private float CheckForBuddiesInterval = 10.0f;

  [SerializeField] private float cohesionCoefficient = 0.1f;

  [SerializeField]
  private float avoidCoefficient = 0.07f;

  [SerializeField]
  private float allignCoefficient = 0.4f;

  [SerializeField] private float separationCoefficient = 200.0f;

  [SerializeField]
  private float forwardDrive = 80.0f;

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

  public int GetBuddyCount()
  {
    return mCurrentBuddies.Count;
  }

  private void UpdateBuddyList()
  {
    GroupTag[] individuals = FindObjectsOfType<GroupTag>();

    for (int count = 0; count < individuals.Length; ++count)
    {
      if (individuals[count].gameObject != gameObject)
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
      Vector3 avoid = Vector3.zero;
      Vector3 cohesion = Vector3.zero;
      //this force compomenet separates different breeds
      Vector3 separation = Vector3.zero;

      int avoidCount = 0;
      cohesionCount = 0;

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
            avoidCount++;
            avoid -= buddyToThis.normalized * (1 / (mag + 0.1f));
          }
        }
        else
        {
          separation -= buddyToThis.normalized * (1 / (mag + 0.1f));
        }
        
      }

      /* Align now is an average velocity of a group.
      *  we should be adding the difference beteween the average velocity and our velocity instead of 
      *  average velocity.
      */
      align = align/mCurrentBuddies.Count - mBody.velocity;
      if (avoidCount > 0)
      {
        //avoid /= avoidCount;
      }
      if (cohesionCount != 0)
      {
        cohesion /= cohesionCount;
      }
      if (!cohesionForceProportionalToDistance)
      {
        cohesion = cohesion.normalized;
      }
      //if (gameObject.name == "Shepherd")
      //{
      //  print("cohesion " + cohesion);
      //  print("budides " + cohesionCount);
      //}
      //Debug.DrawLine(transform.position, transform.position+align*allignCoefficient/70, Color.blue);
      //Debug.DrawLine(transform.position, transform.position + avoid * avoidCoefficient/70, Color.red);
      Debug.DrawLine(transform.position, transform.position + cohesion * cohesionCoefficient/70, Color.yellow);
      Debug.DrawLine(transform.position, transform.position + separation * separationCoefficient / 70, Color.magenta);
      mBody.AddForce((align * allignCoefficient +
        cohesion * cohesionCoefficient +
        avoid * avoidCoefficient +
        separation * separationCoefficient
        ) * Time.deltaTime);
    }
   
  }
}
