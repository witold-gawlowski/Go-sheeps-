using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
  public delegate void LevelCompleteEvent(float duration, LevelButtonScript levelButton);
  public delegate void GameEvent();
  public static event GameEvent OnNewGame;
  public static event GameEvent OnExitGame;
  public static event LevelCompleteEvent OnLevelComplete;


  public enum Screens { TitleScreen, GameScreen, ResultScreen, InstructionsScreen, CreditsScreen, NumScreens }

  private Canvas[] mScreens;
  private Screens mCurrentScreen;

  [SerializeField]
  private Animator cloudAnimator;

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
    transitionFromGame = false;
  }

  public void Update()
  {
    if (Input.GetButtonDown("escape"))
    {
      if (mCurrentScreen == Screens.TitleScreen)
      {
        Application.Quit();
      }
      else if(mCurrentScreen == Screens.GameScreen)
      {
        EndGame();
      }else
      {
        GoToMainMenu();
      }
    }
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

  public void ViewCredits()
  {
    TransitionTo(Screens.CreditsScreen);
  }

  public void LevelComplete(float completionTime)
  {
    OnLevelComplete(completionTime, LevelButtonScript.SelectedButtonScript);
  }

  private bool transitionFromGame;
  private LevelButtonScript lastLevel;

  public void EndGame()
  {
    if (OnExitGame != null)
    {
      OnExitGame();
    }
    transitionFromGame = true;

    TransitionTo(Screens.ResultScreen);
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
  }

  void UnloadGameScenes()
  {
    for(int i=0;i<SceneManager.sceneCount; i++)
    {
      Scene scene = SceneManager.GetSceneAt(i);
      if(scene.name != "Load" && scene.name != "UI")
      {
        SceneManager.UnloadSceneAsync(scene.name);
      }
    }
  }

  private Screens tempScreen;
  public void MakeTransition()
  {
    mScreens[(int)mCurrentScreen].enabled = false;
    mScreens[(int)tempScreen].enabled = true;
    mCurrentScreen = tempScreen;
    if (transitionFromGame)
    {
      UnloadGameScenes();
      SceneManager.LoadSceneAsync("Scenes/Levels/BackgroundLevel", LoadSceneMode.Additive);
      transitionFromGame = false;
    }
  }

  private void TransitionTo(Screens screen)
  {
    tempScreen = screen;
    cloudAnimator.SetTrigger("ChangeScreen");
  }

}
