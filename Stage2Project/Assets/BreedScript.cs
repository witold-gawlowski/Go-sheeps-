using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedScript : MonoBehaviour
{
  [SerializeField]
  private float breedChancePerFrame = 0.0003f;

  [SerializeField]
  private float GrowDuration = 15.0f;

  [SerializeField]
  private GameObject sheep;

  private FlockWithGroup flockScript;
  private GameManager gameManager;
  
  void Start()
  {
    flockScript = GetComponent<FlockWithGroup>();
    gameManager = FindObjectOfType<GameManager>();
  }

  IEnumerator GrowCoroutine()
  {
    transform.localScale = Vector3.zero;
    float counter = 0;
    while (counter < GrowDuration)
    {
      counter += Time.deltaTime;
      transform.localScale = Vector3.one * counter / GrowDuration;
      yield return null;
    }
  }

  void Grow()
  {
    StartCoroutine(GrowCoroutine());
  }

  void Update()
  {
    float chanceToBreedWithAnyBuddyPerFrame = 1 - Mathf.Pow(1 - breedChancePerFrame, flockScript.GetBuddyCount());
    if (Random.value < chanceToBreedWithAnyBuddyPerFrame)
    {
      GameObject spawnedSheep = Instantiate(sheep, transform.position, Quaternion.identity);
      spawnedSheep.transform.parent = gameManager.transform;
      spawnedSheep.GetComponent<BreedScript>().Grow();
    }
   
  }
}
