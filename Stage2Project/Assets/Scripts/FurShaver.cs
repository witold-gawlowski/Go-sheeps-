using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurShaver : MonoBehaviour
{
  [SerializeField]
  private float farmForce = 400.0f;

  [SerializeField]
  private GroupTag.Group farmType = GroupTag.Group.Black;

  [SerializeField]
  private SpriteRenderer farmTypeImage;

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
    FurManager furManager = other.GetComponent<FurManager>();
    if (furManager && furManager.initialAffiliation == farmType)
    {
      furManager.Shave();
    }
  }

  
}
