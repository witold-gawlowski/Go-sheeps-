using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenManager : MonoBehaviour {
  public delegate void WoolChange(int amount);
  static event WoolChange OnWoolChange;

  [SerializeField]
  private Text woolText;
  [SerializeField]
  private Text timeText;
  [SerializeField]
  private float roundDuration = 60;

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
    counter = roundDuration;
  }
  void Update()
  {
    counter -= Time.deltaTime;
    if (counter < 0)
    {
      GetComponentInParent<ScreenManager>().EndGame();
    }
    timeText.text = ((int)counter).ToString() + "s";
  }



  void UpdateWoolText(int total)
  {
    woolText.text = "x " + total.ToString();
  }
}
