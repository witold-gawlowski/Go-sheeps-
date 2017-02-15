using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public enum State { Paused, Playing }

  [SerializeField]
  private GameObject[] SpawnPrefabs;

  [SerializeField]
  private Player PlayerPrefab;

  [SerializeField]
  private Arena Arena;

  [SerializeField]
  private float TimeBetweenSpawns;

  [SerializeField]
  private float BlackSheepFraction = 0.2f;

  private List<GameObject> mObjects;
  private Player[] mPlayer;
  private State mState;
  private float mNextSpawn;
  private Text moneyText;

  void Awake()
  {
    mPlayer = new Player[2];
    mPlayer[0] = Instantiate(PlayerPrefab);
    mPlayer[0].playerID = 1;
    mPlayer[1] = Instantiate(PlayerPrefab);
    mPlayer[1].playerID = 2;
    mPlayer[0].transform.parent = transform;
    mPlayer[1].transform.parent = transform;

    ScreenManager.OnNewGame += ScreenManager_OnNewGame;
    ScreenManager.OnExitGame += ScreenManager_OnExitGame;
  }

  void Start()
  {
    Arena.Calculate();
    mPlayer[0].enabled = false;
    mPlayer[1].enabled = false;
    mState = State.Paused;
  }

  void Update()
  {
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
  }

  private void BeginNewGame()
  {
    if (mObjects != null)
    {
      for (int count = 0; count < mObjects.Count; ++count)
      {
        Destroy(mObjects[count]);
      }
      mObjects.Clear();
    }

    mPlayer[0].transform.position = new Vector3(0.0f, 1.5f, 0.0f);
    mPlayer[1].transform.position = new Vector3(0.0f, 1.5f, 0.0f);
    mPlayer[0].enabled = true;
    mPlayer[1].enabled = true;
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
