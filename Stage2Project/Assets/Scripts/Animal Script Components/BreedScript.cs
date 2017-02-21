using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedScript : MonoBehaviour
{
  [SerializeField]
  private float standardBreedChancePerFrame = 0.0003f;

  [SerializeField]
  private float grassBreedChancePerFrame = 0.0015f;

  [SerializeField]
  private float lambDuration = 15.0f;

  [SerializeField]
  private float growDuration = 15.0f;

  [SerializeField] private float initialSize = 0.3f;

  [SerializeField]
  private int stopBreedingBuddyCount;

  private FlockWithGroup flockScript;
  private GameManager gameManager;
  private GroupTag groupTag;
  private bool isOnGrass;
  
  void Start()
  {
    flockScript = GetComponent<FlockWithGroup>();
    gameManager = FindObjectOfType<GameManager>();
    groupTag = GetComponent<GroupTag>();
  }

  IEnumerator GrowCoroutine()
  {
    transform.localScale = Vector3.one* initialSize;
    float counter = 0;
    yield return new WaitForSeconds(lambDuration);
    while (counter < growDuration)
    {
      counter += Time.deltaTime;
      transform.localScale = Vector3.one * Mathf.Lerp(initialSize,1, counter / growDuration);
      yield return null;
    }
    GetComponent<HealthScript>().SetHealth(4);
  }

  public void EnterGrass()
  {
    isOnGrass = true;
  }

  public void ExitGrass()
  {
    isOnGrass = false;
  }

  void Grow()
  {
    StartCoroutine(GrowCoroutine());
  }

  void Breed()
  {
    if (groupTag.Affiliation == GroupTag.Group.Shaved)
    {
      return;
    }
    if (flockScript.GetBuddyCount() > stopBreedingBuddyCount)
    {
      return;
    }
    float breedChancePerFrame = isOnGrass ? grassBreedChancePerFrame : standardBreedChancePerFrame;
    float chanceToBreedWithAnyBuddyPerFrame = 1 - Mathf.Pow(1 - breedChancePerFrame, flockScript.GetBuddyCount());
    if (Random.value < chanceToBreedWithAnyBuddyPerFrame)
    {
      GameObject spawnedSheep = Instantiate(this.gameObject, transform.position, Quaternion.identity);
      spawnedSheep.GetComponent<HealthScript>().SetHealth(1);
      spawnedSheep.transform.parent = gameManager.transform;
      spawnedSheep.GetComponent<BreedScript>().Grow();
    }
  }

  void Update()
  {
    Breed();
  }
}
