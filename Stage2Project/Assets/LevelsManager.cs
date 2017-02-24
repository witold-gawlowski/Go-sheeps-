using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
  [SerializeField]
  private LevelButtonScript firstLevel;

  private List<LevelButtonScript> levels;

  void RegisterLevels()
  {
    LevelButtonScript nextLevelButton = firstLevel;
    while (true)
    {
      levels.Add(nextLevelButton);
      nextLevelButton = nextLevelButton.GetNextLevel();
      if (nextLevelButton == null)
      {
        return;
      }
    }
  }

  public void StartGame()
  {
    print(LevelButtonScript.SelectedButtonScript.levelName);
    SceneManager.LoadSceneAsync(LevelButtonScript.SelectedButtonScript.levelName, LoadSceneMode.Additive);
    
  }

  void Start()
  {
    levels = new List<LevelButtonScript>();
    RegisterLevels();
    levels[0].Select();
    ScreenManager.OnLevelComplete += LevelComplete;
    LevelButtonScript.OnLevelChange += ChangeLevel;
  }

  private void ChangeLevel()
  {
    print("Update GUI");
  }

  public void LevelComplete()
  {
    LevelButtonScript.SelectedButtonScript.Complete();
  }

}
