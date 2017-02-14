using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTag : MonoBehaviour
{
  public enum Group { White, Black, Shaved}

  public Material[] materials;

  [SerializeField]
  private Group GroupCode;

  public Group Affiliation
  {
    get { return GroupCode; }
    set { GroupCode = value; }
  }
}
