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

  IEnumerator LoadSceneOnUnload(AsyncOperation op)
  {
    while (!op.isDone)
    {
      yield return null;
    }
    SceneManager.LoadSceneAsync(LevelButtonScript.SelectedButtonScript.levelName, LoadSceneMode.Additive);
  }
  public void StartGame()
  {
    AsyncOperation op = SceneManager.UnloadSceneAsync("Scenes/Levels/BackgroundLevel");
    StartCoroutine(LoadSceneOnUnload(op));
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
  }

  public void LevelComplete(float ignore)
  {
    LevelButtonScript.SelectedButtonScript.Complete(0.0f);

  }

}
