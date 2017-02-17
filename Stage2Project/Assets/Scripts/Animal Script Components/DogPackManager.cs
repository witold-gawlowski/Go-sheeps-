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

  [HideInInspector]
  public ShepherdShop CurrentShop;

  void Start()
  {
    Affiliation = GetComponent<GroupTag>().Affiliation;
   UpdateShepards();
  }

  public int GetPackSize()
  {
    return shepards.Count;
  }

  public void UpdateShepards()
  {
    shepards = FindObjectsOfType<ShepherdScript>().Where(
     item => item.InitialAffiliation == Affiliation
     ).ToList();
  }

  //todo: move this code to balls of yarn.
  public void Purchase()
  {
    if (CurrentShop && FurShaver.BallsOfYarn >= CurrentShop.GetPrice())
    {
      FurShaver.BallsOfYarn -= CurrentShop.GetPrice();
      GameObject shepard = Instantiate(shepardPrefab, transform.position, Quaternion.identity);
      GroupTag groupTag = shepard.GetComponent<GroupTag>();
      groupTag.Affiliation = Affiliation;
      shepards.Add(shepard.GetComponent<ShepherdScript>());
      CurrentShop.UpdateGUI();
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
