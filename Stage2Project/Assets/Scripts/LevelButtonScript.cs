using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
  public delegate void GameEvent();
  public static event GameEvent OnLevelChange;

  [SerializeField]
  private LevelButtonScript nextLevel;

  [SerializeField]
  private Image highlightImage;

  [SerializeField]
  private Sprite thumbnail;

  [SerializeField]
  private Image thumbnailImage;

  [SerializeField]
  private string levelName;

  [SerializeField]
  private string levelPathPrefix = "Scenes/Levels/Tutorial/";

  private static LevelButtonScript selectedButtonScript;

  public static LevelButtonScript GetSelectedButtonScript()
  {
    return selectedButtonScript;
  }

  public void Start()
  {
    ScreenManager.OnLevelComplete += LevelCompleteHandler;
    OnLevelChange += Desselect;
  }

  public string GetFullLevelName()
  {
    return levelPathPrefix + levelName;
  }

  public string GetLevelName()
  {
    return levelName;
  }

  public LevelButtonScript GetNextLevel()
  {
    return nextLevel;
  }

  public void LevelCompleteHandler(float time, LevelButtonScript levelButton)
  {
    if (levelButton == this)
    {
      if (nextLevel)
      {
        nextLevel.gameObject.SetActive(true);
        nextLevel.Select();
      }
    }
   
  }

  public void Select()
  {
    selectedButtonScript = this;
    thumbnailImage.sprite = thumbnail;

    OnLevelChange();
    highlightImage.enabled = true;
  }

  public void Desselect()
  {
    highlightImage.enabled = false;
  }
}
