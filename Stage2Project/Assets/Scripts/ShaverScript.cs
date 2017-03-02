using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaverScript : MonoBehaviour
{
  [SerializeField]
  private GroupTag.Group farmType = GroupTag.Group.Black;

  [SerializeField]
  private SpriteRenderer farmTypeImage;

  private static int _BallsOfYarn;
  public static int BallsOfYarn {
    get { return _BallsOfYarn; } 
    set
    {
      _BallsOfYarn = value;
      GameScreenManager.ChangeWool(value);
    } 
  }

  void Start()
  {
    _BallsOfYarn = 0;
  }

  void OnValidate()
  {
    if (!farmTypeImage)
    {
      return;
    }
    if (farmType == GroupTag.Group.White)
    {
      farmTypeImage.color = Color.white;
      
    }
    else
    {
      farmTypeImage.color = Color.black;
    }
  }

  public void OnTriggerEnter(Collider other)
  {
    FurScript furManager = other.GetComponent<FurScript>();
    if (furManager && furManager.furColor == farmType && ! furManager.IsShaved())
    {
      furManager.Shave();
      BallsOfYarn++;
    }
  }

  
}
