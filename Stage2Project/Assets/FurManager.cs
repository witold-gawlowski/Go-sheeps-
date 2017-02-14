using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurManager : MonoBehaviour
{

  [SerializeField]
  private float shavedBellySize = 0.7f;

  [SerializeField]
  private float grownFurWidth;

  [SerializeField]
  private Material shavedMaterial;

  [SerializeField]
  private Material fullHairMaterial;

  private int furState = 1;
  private MeshRenderer meshRenderer;
  private Transform bodyTransform;
  private GroupTag groupTag;
  private GroupTag.Group initialAffiliation;
  private GameManager gameManager;

  void Awake()
  {
    meshRenderer = GetComponent<MeshRenderer>();
    groupTag = GetComponentInParent<GroupTag>();
    initialAffiliation = groupTag.Affiliation;
    if (groupTag.Affiliation == GroupTag.Group.Shaved)
    {
      throw new Exception("Sheep can't be shaved from the start.");
    }
  }

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
  }

  public void Shave()
  {
    if (furState == 0)
    {
      return;
    }
    meshRenderer.material = shavedMaterial;
    transform.localScale = new Vector3(shavedBellySize, shavedBellySize, 1);
    furState = 0;
    groupTag.Affiliation  = GroupTag.Group.Shaved;
  }

  public void Grow()
  {
    if (furState == 1)
    {
      return;
    }
    meshRenderer.material = fullHairMaterial;
    transform.localScale = new Vector3(grownFurWidth, grownFurWidth, 0);
    furState = 1;
    groupTag.Affiliation = initialAffiliation;
  }

}
