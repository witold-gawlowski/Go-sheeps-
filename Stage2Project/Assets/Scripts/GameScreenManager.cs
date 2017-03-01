using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenManager : MonoBehaviour {
  public delegate void WoolChange(int amount);

  static event WoolChange OnWoolChange;

  [SerializeField]
  private Text woolText;
  [SerializeField]
  private Text timeText;

  private bool levelComplete;
  private float counter;

  public static void ChangeWool(int total)
  {
    if (OnWoolChange != null)
    {
      OnWoolChange(total);
    }
  }
  void Awake()
  {
    OnWoolChange += UpdateWoolText;
    OnWoolChange += CheckWinningCondition;
    ScreenManager.OnNewGame += NewRound;
  }
  void Start()
  {
    GameManager.GameManagerInitializedEvent += NewRound;
  }
  
  void NewRound()
  {
    counter = 0;
    UpdateWoolText(0);
    levelComplete = false;
  }
  void Update()
  {
    counter += Time.deltaTime;
    timeText.text = counter.ToString("0.00") + "s";
  }

  void CheckWinningCondition(int total)
  {
    if (total >= GameManager.GetCurrentTargetWool() && ! levelComplete)
    {
      levelComplete = true;
      ScreenManager screenManager = GetComponentInParent<ScreenManager>();
      screenManager.EndGame();
      screenManager.LevelComplete(counter);
    }
  }


  public void UpdateWoolText(int total)
  {
    woolText.text = "x " + total.ToString() + "/" + GameManager.GetCurrentTargetWool();
  }
}
