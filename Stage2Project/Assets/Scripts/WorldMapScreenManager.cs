using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldMapScreenManager : MonoBehaviour
{
  [SerializeField]
  private LevelButtonScript firstLevel;

  [SerializeField]
  private Slider playerNumberSlider;

  [SerializeField]
  private InputField playerNameField;

  [SerializeField]
  private Text levelName;

  public List<LevelButtonScript> levels;

  public static int PlayerNumber;

  public void OnSliderChange()
  {
    PlayerNumber = (int)playerNumberSlider.value;
  }

  public void Awake()
  {
    OnSliderChange();
  }

  void Start()
  {
    ScreenManager.OnLevelComplete += UnlockNewLevel;
    LevelButtonScript.OnLevelChange += UpdateLevelName;

    firstLevel.Select();
    LoadSavedLevelState();
    string playerName = PlayerPrefs.GetString("PlayerName");

    if (playerName != "")
    {
      playerNameField.text = playerName;
    }

  }

  public void UpdateLevelName()
  {
    levelName.text = LevelButtonScript.GetSelectedButtonScript().GetLevelName();
  }

  public void SaveName()
  {
    PlayerPrefs.SetString("PlayerName", playerNameField.text);
  }

  void LoadSavedLevelState()
  {
    string reachedLevel = PlayerPrefs.GetString("ReachedLevel");
    LevelButtonScript currentLevel = LevelButtonScript.GetSelectedButtonScript();

    if (reachedLevel == "")
    {
      PlayerPrefs.SetString("ReachedLevel", currentLevel.GetLevelName());
      return;
    }

    while (currentLevel != null)
    {
      currentLevel.gameObject.SetActive(true);
      string currentLevelName = currentLevel.GetLevelName();
      if(currentLevelName == reachedLevel)
      {
        return;
      }
      currentLevel = currentLevel.GetNextLevel();      
    }
  }

  IEnumerator LoadSceneOnUnloadOld(AsyncOperation op)
  {
    while (!op.isDone)
    {
      yield return null;
    }
    SceneManager.LoadSceneAsync(LevelButtonScript.GetSelectedButtonScript().GetFullLevelName(), LoadSceneMode.Additive);
  }

  public void StartGame()
  {
    AsyncOperation op = SceneManager.UnloadSceneAsync("Scenes/Levels/BackgroundLevel");
    StartCoroutine(LoadSceneOnUnloadOld(op));
  }



  public void UnlockNewLevel(float ignore, LevelButtonScript finishedLevelButton)
  {
    if(finishedLevelButton.GetLevelName() == PlayerPrefs.GetString("ReachedLevel"))
    {
      LevelButtonScript nextLevelButton = finishedLevelButton.GetNextLevel();
      if (nextLevelButton) {
        PlayerPrefs.SetString("ReachedLevel", nextLevelButton.GetLevelName());
      }
    }
  }

}
