using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenManager : MonoBehaviour {
  public delegate void WoolChange(int amount);
  static event WoolChange OnWoolChange;

  [SerializeField]
  private Text woolText;

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
  }

  void UpdateWoolText(int total)
  {
    woolText.text = "x " + total.ToString();
  }
}
