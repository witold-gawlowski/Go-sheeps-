using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
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

  public void UpdateLevelName()
  {
    levelName.text = LevelButtonScript.SelectedButtonScript.GetLevelName();
  }

  public void SaveName()
  {
    PlayerPrefs.SetString("PlayerName", playerNameField.text);
    print("save name" + PlayerPrefs.GetString("PlayerName"));
  }

  void LoadSavedLevelState()
  {
    string reachedLevel = PlayerPrefs.GetString("ReachedLevel");
    LevelButtonScript currentLevel = LevelButtonScript.SelectedButtonScript;
    if (reachedLevel == "")
    {
      PlayerPrefs.SetString("ReachedLevel", currentLevel.GetLevelName());
      return;
    }
    while (true)
    {
      currentLevel.gameObject.SetActive(true);
      string currentLevelName = currentLevel.GetLevelName();
      if(currentLevelName == reachedLevel)
      {
        return;
      }
      currentLevel = currentLevel.GetNextLevel();
      if (currentLevel == null)
      {
        print("exit " + currentLevelName);
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
    SceneManager.LoadSceneAsync(LevelButtonScript.SelectedButtonScript.GetFullLevelName(), LoadSceneMode.Additive);
  }

  public void StartGame()
  {
    AsyncOperation op = SceneManager.UnloadSceneAsync("Scenes/Levels/BackgroundLevel");
    StartCoroutine(LoadSceneOnUnload(op));
  }

  void Start()
  {
    //print(PlayerPrefs.GetString("ReachedLevel") + " = reached level");
    levels = new List<LevelButtonScript>();
    RegisterLevels();
    ScreenManager.OnLevelComplete += LevelComplete;
    LevelButtonScript.OnLevelChange += UpdateLevelName;
    levels[0].Select();
    LoadSavedLevelState();
    string playerName = PlayerPrefs.GetString("PlayerName");
   
    if (playerName != "") {
      playerNameField.text = playerName;
    }

  }

  public void LevelComplete(float ignore, LevelButtonScript levelButton)
  {
    if(levelButton.GetLevelName() == PlayerPrefs.GetString("ReachedLevel"))
    {
      LevelButtonScript nextLevelButton = levelButton.GetNextLevel();
      if (nextLevelButton) {
        PlayerPrefs.SetString("ReachedLevel", nextLevelButton.GetLevelName());
      }
    }
  }

}
