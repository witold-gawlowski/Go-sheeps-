﻿using System;
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

  private int furState = 1;
  private MeshRenderer meshRenderer;
  private Transform bodyTransform;
  private GroupTag groupTag;
  private HealthScript healthScript;
  
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

  void Start()
  {
    healthScript = GetComponentInParent<HealthScript>();
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
    Invoke("Grow", furGrowthDuration);
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