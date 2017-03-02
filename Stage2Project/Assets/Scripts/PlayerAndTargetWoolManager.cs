using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAndTargetWoolManager : MonoBehaviour
{
  public delegate void GameEvent();
  public static event GameEvent GameManagerInitializedEvent;

  [SerializeField]
  private Player PlayerPrefab;

  [SerializeField]
  private Arena Arena;

  [SerializeField]
  private int targetWool = 15;
  private Player[] mPlayer;
  private GameObject playerStart;

  private static int staticTargetWool;

  private Player GetPlayer(GroupTag.Group group, int id)
  {
    Player result = Instantiate(PlayerPrefab);
    result.transform.position = new Vector3(playerStart.transform.position.x, 0.5f, playerStart.transform.position.z);
    result.playerID = id;
    result.transform.parent = transform;
    result.GetComponent<GroupTag>().Affiliation = group;
    return result;
  }
  void Awake()
  {
    staticTargetWool = targetWool;
    playerStart = GameObject.FindGameObjectWithTag("PlayerStart");

    mPlayer = new Player[2];
    mPlayer[0] = GetPlayer(GroupTag.Group.Dogs1, 1);
    mPlayer[1] = GetPlayer(GroupTag.Group.Dogs2, 2);
  }

  void Start()
  {
    Arena.Calculate();

    if (LevelsManager.PlayerNumber == 2)
    {
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
}
