using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DogPackManager : MonoBehaviour
{
  [SerializeField]
  private GameObject shepardPrefab;
  //todo: make it private
  public List<ShepherdScript> shepards;
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
     ).ToList();
  }

  public void Buy()
  {
    print("buy");
    if (FurShaver.BallsOfYarn > -31)
    {
      FurShaver.BallsOfYarn -= 30;
      GameObject shepard = Instantiate(shepardPrefab, transform.position, Quaternion.identity);
      GroupTag groupTag = shepard.GetComponent<GroupTag>();
      groupTag.Affiliation = Affiliation;
      shepards.Add(shepard.GetComponent<ShepherdScript>());
    }
  }

  public void SetGuard()
  {
    for (int i = 0; i < shepards.Count; i++)
    {
      if (!shepards[i].IsOnStayCommand)
      {
        shepards[i].Stay();
        break;
      }
    }
  }
}
