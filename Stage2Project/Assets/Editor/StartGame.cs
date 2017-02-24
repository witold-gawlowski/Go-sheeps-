using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

//copied from: http://answers.unity3d.com/questions/441246/editor-script-to-make-play-always-jump-to-a-start.html

public class StartGame : MonoBehaviour {
  // class doesn't matter, add to anything in the Editor folder
  // for any beginners reading, this is c#

  [MenuItem("Edit/Play-Stop, But From Prelaunch Scene %0")]
  public static void PlayFromPrelaunchScene()
  {
    if (EditorApplication.isPlaying == true)
    {
      EditorApplication.isPlaying = false;
      return;
    }

    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    EditorSceneManager.OpenScene("Assets/Scenes/Load.unity");
    EditorApplication.isPlaying = true;
  }

  [MenuItem("Edit/Open main scene %9")]
  public static void OpenMainScene()
  {
    if (EditorApplication.isPlaying == true)
    {
      EditorApplication.isPlaying = false;
      return;
    }

    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    EditorSceneManager.OpenScene("Assets/Scenes/UI");
  }
}
