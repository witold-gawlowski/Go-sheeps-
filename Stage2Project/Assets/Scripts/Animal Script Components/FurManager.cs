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

  [SerializeField]
  private float furGrowthDuration = 4.0f;

  [SerializeField]
  private float grassFurGrowthSpeedMultiplier = 3.5f;

  private int furState = 1;
  private MeshRenderer meshRenderer;
  private GroupTag groupTag;
  private HealthScript healthScript;
  private bool isOnGrass;

  [HideInInspector]
  public GroupTag.Group furColor;

  void Awake()
  {
    meshRenderer = GetComponent<MeshRenderer>();
    groupTag = GetComponentInParent<GroupTag>();

    furColor = groupTag.Affiliation;
    if (groupTag.Affiliation == GroupTag.Group.Shaved)
    {
      throw new Exception("Sheep can't be shaved from the start.");
    }
  }



  public bool IsShaved()
  {
    if (furState == 0)
    {
      return true;
    }
    return false;
  }
  void Start()
  {
    healthScript = GetComponentInParent<HealthScript>();
  }

  public void EnterGrass()
  {
    isOnGrass = true;
  }

  public void ExitGrass()
  {
    isOnGrass = false;
  }

  public void Shave()
  {
    healthScript.LooseHealth();
    if (furState == 0)
    {
      return;
    }
    meshRenderer.material = shavedMaterial;
    transform.parent.localScale = new Vector3(shavedBellySize, shavedBellySize, 1);
    furState = 0;
    groupTag.Affiliation = GroupTag.Group.Shaved;
    StartCoroutine(GrowCoroutine());
  }

  private IEnumerator GrowCoroutine()
  {
    float counter = furGrowthDuration;
    while (counter > 0)
    {
      counter -= Time.deltaTime * (isOnGrass  ? grassFurGrowthSpeedMultiplier : 1.0f);
      yield return null;
    }
    Grow();
  }

  public void Grow()
  {
    if (furState == 1)
    {
      return;
    }
    meshRenderer.material = fullHairMaterial;
    transform.parent.localScale = new Vector3(grownFurWidth, grownFurWidth, 1);
    furState = 1;
    groupTag.Affiliation = furColor;
  }

}
