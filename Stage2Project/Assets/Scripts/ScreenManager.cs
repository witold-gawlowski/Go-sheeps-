using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
  public delegate void LevelCompleteEvent(float duration);
  public delegate void GameEvent();
  public static event GameEvent OnNewGame;
  public static event GameEvent OnExitGame;
  public static event LevelCompleteEvent OnLevelComplete;


  public enum Screens { TitleScreen, GameScreen, ResultScreen, InstructionsScreen, NumScreens }

  private Canvas[] mScreens;
  private Screens mCurrentScreen;

  void Awake()
  {
    mScreens = new Canvas[(int)Screens.NumScreens];
    Canvas[] screens = GetComponentsInChildren<Canvas>();
    for (int count = 0; count < screens.Length; ++count)
    {
      for (int slot = 0; slot < mScreens.Length; ++slot)
      {
        if (mScreens[slot] == null && ((Screens)slot).ToString() == screens[count].name)
        {
          mScreens[slot] = screens[count];
          break;
        }
      }
    }

    for (int screen = 1; screen < mScreens.Length; ++screen)
    {
      mScreens[screen].enabled = false;
    }

    mCurrentScreen = Screens.TitleScreen;
  }




  public void StartGame()
  {
    if (OnNewGame != null)
    {
      OnNewGame();
    }
    TransitionTo(Screens.GameScreen);
  }

  public void ViewInstructions()
  {
    TransitionTo(Screens.InstructionsScreen);
  }

  public void LevelComplete(float completionTime)
  {
    OnLevelComplete(completionTime);
  }

  public void EndGame()
  {
    if (OnExitGame != null)
    {
      OnExitGame();
    }
    TransitionTo(Screens.ResultScreen);
    SceneManager.UnloadSceneAsync(LevelButtonScript.SelectedButtonScript.levelName);
    SceneManager.LoadSceneAsync("Scenes/Levels/BackgroundLevel", LoadSceneMode.Additive);

  }

  public void GoToLevelSelection()
  {
    TransitionTo(Screens.ResultScreen);
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void GoToMainMenu()
  {
    TransitionTo(Screens.TitleScreen);
    //SceneManager.LoadSceneAsync("Scenes/Levels/BackgroundLevel", LoadSceneMode.Additive);
  }

  private void TransitionTo(Screens screen)
  {
    mScreens[(int)mCurrentScreen].enabled = false;
    mScreens[(int)screen].enabled = true;
    mCurrentScreen = screen;
  }


}
