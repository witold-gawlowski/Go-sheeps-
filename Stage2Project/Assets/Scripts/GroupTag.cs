using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTag : MonoBehaviour
{
  public enum Group { White, Black, Shaved, Dogs1, Dogs2, Dogs3, Dogs4, Dogs5}

  [SerializeField]
  private Group GroupCode;

  public Group Affiliation
  {
    get { return GroupCode; }
    set { GroupCode = value; }
  }
}
