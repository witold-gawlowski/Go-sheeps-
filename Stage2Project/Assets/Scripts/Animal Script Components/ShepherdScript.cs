using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlockWithGroup))]
[RequireComponent(typeof(GroupTag))]
public class ShepherdScript : MonoBehaviour
{
  public float StayDuration = 10.0f;
  public bool IsOnStayCommand { get; set; }
  private GroupTag groupTag;
  public GroupTag.Group InitialAffiliation { get; private set; }

  private FlockWithGroup flockWithGroup;

  void Awake()
  {
    IsOnStayCommand = false;
  }

  void Start()
  {
    flockWithGroup = GetComponent<FlockWithGroup>();
    groupTag = GetComponent<GroupTag>();
    InitialAffiliation = GetComponent<GroupTag>().Affiliation;
    if (InitialAffiliation == GroupTag.Group.DogsOnStayCommand)
    {
      throw new Exception("Invalid initial shepard affiliation.");
    }
  }

  IEnumerator StayCoroutine()
  {
    IsOnStayCommand = true;
    flockWithGroup.enabled = false;
    groupTag.Affiliation = GroupTag.Group.DogsOnStayCommand;
    yield return new WaitForSeconds(StayDuration);
    IsOnStayCommand = false;
    flockWithGroup.enabled = true;
    groupTag.Affiliation = InitialAffiliation;
  }
  public void Stay()
  {
    StartCoroutine(StayCoroutine());
  }
}
