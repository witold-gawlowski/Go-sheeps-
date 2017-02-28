using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
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

  public delegate void GameEvent();
  public static event GameEvent OnLevelChange;
  public static LevelButtonScript SelectedButtonScript;

  public void Start()
  {
    ScreenManager.OnLevelComplete += Complete;
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

  public void Complete(float ignore)
  {
    if (SelectedButtonScript == this)
    {
      if (nextLevel)
      {
        nextLevel.gameObject.SetActive(true);
      }
    }
   
  }

  public void Select()
  {
    SelectedButtonScript = this;
    thumbnailImage.sprite = thumbnail;
    OnLevelChange();
    highlightImage.enabled = true;
  }

  public void Desselect()
  {
    highlightImage.enabled = false;
  }
}
