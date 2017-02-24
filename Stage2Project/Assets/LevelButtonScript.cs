using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
  private string levelName;

  public delegate void GameEvent();
  public static event GameEvent OnLevelChange;
  public static LevelButtonScript SelectedButtonScript;

  public void Start()
  {
    ScreenManager.OnLevelComplete += Complete;
    OnLevelChange += Desselect;
  }

  public void StartGame()
  {
    print(LevelButtonScript.SelectedButtonScript.levelName);
    //SceneManager.UnloadSceneAsync();
    SceneManager.LoadScene(LevelButtonScript.SelectedButtonScript.levelName);
  }

  public LevelButtonScript GetNextLevel()
  {
    return nextLevel;
  }

  public void Complete()
  {
    nextLevel.gameObject.SetActive(true);
  }

  public void Select()
  {
    OnLevelChange();
    highlightImage.enabled = true;
    SelectedButtonScript = this;
  }

  public void Desselect()
  {
    highlightImage.enabled = false;
  }
}
