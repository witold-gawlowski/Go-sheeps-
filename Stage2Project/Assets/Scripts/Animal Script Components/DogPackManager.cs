using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DogPackManager : MonoBehaviour
{
  public ShepherdScript[] shepards;
  public GroupTag.Group Affiliation { get; private set; }

  void Start()
  {
    Affiliation = GetComponent<GroupTag>().Affiliation;
   UpdateShepards();
  }

  public void UpdateShepards()
  {
    shepards = FindObjectsOfType<ShepherdScript>().Where(
     item => item.InitialAffiliation == Affiliation
     ).ToArray();
  }

  public void SetGuard()
  {
    for (int i = 0; i < shepards.Length; i++)
    {
      if (!shepards[i].IsOnStayCommand)
      {
        shepards[i].Stay();
        break;
      }
    }
  }
}
