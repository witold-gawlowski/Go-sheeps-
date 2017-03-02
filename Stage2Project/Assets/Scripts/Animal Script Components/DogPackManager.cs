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
    if (CurrentShop && ShaverScript.BallsOfYarn >= CurrentShop.GetPrice())
    {
      ShaverScript.BallsOfYarn -= CurrentShop.GetPrice();
      GameObject shepardObject = Instantiate(shepardPrefab, transform.position, Quaternion.identity);
      GroupTag groupTag = shepardObject.GetComponent<GroupTag>();
      groupTag.Affiliation = Affiliation;
      ShepherdScript shepardScript = shepardObject.GetComponent<ShepherdScript>();
      shepards.Add(shepardScript);
      MagnetizedByPlayer.RegisterShepard(shepardScript.gameObject);
      CurrentShop.UpdateGUI();
    }
  }

  public void Summon()
  {
    for (int i = 0; i < shepards.Count; i++)
    {
      if (shepards[i].IsOnStayCommand)
      {
        shepards[i].Summon();
        break;
      }
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
