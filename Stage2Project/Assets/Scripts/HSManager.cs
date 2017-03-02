using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//I was using theee resources while making highscores:
//http://wiki.unity3d.com/index.php?title=Server_Side_Highscores
//https://www.scirra.com/tutorials/4839/creating-your-own-leaderboard-highscores-easy-and-free-php-mysql/page-1

public class HSManager : MonoBehaviour
{
  [SerializeField]
  private Text playerName;
  [SerializeField]
  private Text highscoresText;

  private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
  public string addScoreURL = "https://sheeps-searchforastar.000webhostapp.com/addscore.php"; //be sure to add a ? to your url
  public string highscoreURL = "https://sheeps-searchforastar.000webhostapp.com/display.php";

  void Start()
  {
    StartCoroutine(GetScores(LevelButtonScript.GetSelectedButtonScript().GetLevelName()));
    ScreenManager.OnLevelComplete += HandleScores;
    ScreenManager.OnExitGame += DisplayScore;
    ScreenManager.OnNewGame += DisplayScore;
    LevelButtonScript.OnLevelChange += DisplayScore;
  }

  public void HandleScores(float completionTime, LevelButtonScript ignore)
  {
    StartCoroutine(PostScores(playerName.text, completionTime, LevelButtonScript.GetSelectedButtonScript().GetLevelName()));
  }

  public void DisplayScore()
  {
    StartCoroutine(GetScores(LevelButtonScript.GetSelectedButtonScript().GetLevelName()));
  }


    // remember to use StartCoroutine when calling this function!
  IEnumerator PostScores(string name, float score, string levelName)
  {
    //This connects to a server side php script that will add the name and score to a MySQL DB.
    // Supply it with a string representing the players name and the players score.
    //string hash = MD5Test.Md5Sum(name + score + secretKey);

    string post_url = addScoreURL + "?name=" + WWW.EscapeURL(name) + "&score=" + score +
      "&levelID=" + WWW.EscapeURL(levelName);// + " & hash=" + hash;
    //print(post_url);
    // Post the URL to the site and create a download object to get the result.
    WWW hs_post = new WWW(post_url);
    yield return hs_post; // Wait until the download is done

    if (hs_post.error != null)
    {
      print("There was an error posting the high score: " + hs_post.error);
    }
  }

  // Get the scores from the MySQL DB to display in a GUIText.
  // remember to use StartCoroutine when calling this function!
  IEnumerator GetScores(string levelName)
  {
    highscoresText.text = "Loading Scores...";
    string get_url = highscoreURL + "?levelID=" + WWW.EscapeURL(levelName);
    //print(get_url);
    WWW hs_get = new WWW(get_url);
    yield return hs_get;

    if (hs_get.error != null)
    {
      print("There was an error getting the high score: " + hs_get.error);
    }
    else
    {
      highscoresText.text = hs_get.text; // this is a GUIText that will display the scores in game.
    }
  }

}