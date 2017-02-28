using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public delegate void GameEvent();
  public static event GameEvent GameManagerInitializedEvent;

  public enum State { Paused, Playing }

  [SerializeField]
  private Player PlayerPrefab;

  [SerializeField]
  private Arena Arena;

  [SerializeField]
  private float TimeBetweenSpawns;

  [SerializeField]
  private float BlackSheepFraction = 0.2f;

  [SerializeField]
  private int targetWool = 15;

  private List<GameObject> mObjects;
  private Player[] mPlayer;
  private State mState;
  private float mNextSpawn;
  private Text moneyText;
  private GameObject playerStart;

  private static int staticTargetWool;

  void Awake()
  {
    staticTargetWool = targetWool;
    playerStart = GameObject.FindGameObjectWithTag("PlayerStart");
    mPlayer = new Player[2];
    mPlayer[0] = Instantiate(PlayerPrefab);
    mPlayer[0].transform.position = new Vector3(playerStart.transform.position.x, 0f, playerStart.transform.position.z);
    mPlayer[0].playerID = 1;
    mPlayer[0].transform.parent = transform;

    mPlayer[1] = Instantiate(PlayerPrefab);
    mPlayer[1].transform.position = new Vector3(playerStart.transform.position.x+1.5f, 0f, playerStart.transform.position.z);
    mPlayer[1].playerID = 2;
    mPlayer[1].transform.parent = transform;

    ScreenManager.OnNewGame += ScreenManager_OnNewGame;
    ScreenManager.OnExitGame += ScreenManager_OnExitGame;

  }

  void Start()
  {
    Arena.Calculate();
    mPlayer[0].GetComponent<GroupTag>().Affiliation = GroupTag.Group.Dogs1;
    mPlayer[1].GetComponent<GroupTag>().Affiliation = GroupTag.Group.Dogs2;
    mState = State.Paused;
    if (LevelsManager.PlayerNumber == 2)
    {
      print("two" + LevelsManager.PlayerNumber);
      mPlayer[1].gameObject.SetActive(true);
    }
    else
    {
      mPlayer[1].gameObject.SetActive(false);
    }
    GameManagerInitializedEvent();
  }

  public static int GetCurrentTargetWool()
  {
    return staticTargetWool;
  }

  void OnDestroy()
  {
    ScreenManager.OnNewGame -= ScreenManager_OnNewGame;
    ScreenManager.OnExitGame -= ScreenManager_OnExitGame;
  }

  void Update()
  {
    /* I now spawn boids in BreedScript.
    if (mState == State.Playing)
    {
      mNextSpawn -= Time.deltaTime;
      if (mNextSpawn <= 0.0f)
      {
        if (mObjects == null)
        {
          mObjects = new List<GameObject>();
        }

        int indexToSpawn = Random.value > BlackSheepFraction ? 0 : 1;
        GameObject spawnObject = SpawnPrefabs[indexToSpawn];
        GameObject spawnedInstance = Instantiate(spawnObject);
        spawnedInstance.transform.parent = transform;
        mObjects.Add(spawnedInstance);
        mNextSpawn = TimeBetweenSpawns;
      }
    }
    */
  }

  //this is now never called. 
  private void BeginNewGame()
  {
    print("begin game");
    if (mObjects != null)
    {
      for (int count = 0; count < mObjects.Count; ++count)
      {
        Destroy(mObjects[count]);
      }
      mObjects.Clear();
    }

    mPlayer[0].transform.position = new Vector3(0.0f, 0.5f, 0.0f);
    mPlayer[0].enabled = true;
    mPlayer[1].transform.position = new Vector3(0.0f, 0.5f, 0.0f);
    
    mNextSpawn = TimeBetweenSpawns;

    mState = State.Playing;
  }

  private void EndGame()
  {
    mPlayer[0].enabled = false;
    mPlayer[1].enabled = false;
    mState = State.Paused;
  }

  private void ScreenManager_OnNewGame()
  {
    BeginNewGame();
  }

  private void ScreenManager_OnExitGame()
  {
    EndGame();
  }
}
