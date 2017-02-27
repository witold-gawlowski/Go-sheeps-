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

  public string levelName;

  public delegate void GameEvent();
  public static event GameEvent OnLevelChange;
  public static LevelButtonScript SelectedButtonScript;

  public void Start()
  {
    ScreenManager.OnLevelComplete += Complete;
    OnLevelChange += Desselect;
  }


  public LevelButtonScript GetNextLevel()
  {
    return nextLevel;
  }

  public void Complete(float ignore)
  {
    if (SelectedButtonScript == this)
    {
      nextLevel.gameObject.SetActive(true);
    }
  }

  public void Select()
  {
    OnLevelChange();
    highlightImage.enabled = true;
    SelectedButtonScript = this;
    thumbnailImage.sprite = thumbnail;
  }

  public void Desselect()
  {
    highlightImage.enabled = false;
  }
}
