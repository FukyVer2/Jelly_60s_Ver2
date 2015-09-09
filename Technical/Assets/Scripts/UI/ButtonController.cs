using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public GameObject gameStart;
    public GameObject gamePlay;
    public GameOver gameOver;
    public GameObject hightScore;
    public Text txtHightScore;

    public AudioSource audioGamePlay;
    public FinishTutorial finishTutorial;

    private GameController _gameController;
    private UIController _uiController;
    private GameOver gameOverController;
    private float hightPointScore;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GamePlay()//Game Play
    {
        //#if UNITY_EDITOR
        //        Debug.Log("Show Banner");
#if UNITY_ANDROID || UNITY_IOS
        Debug.Log("Show Banner");
        GoogleAds_J60s.Instance.ShowBanner();
#endif
        MyApplication.Instance.googleAnalytics.LogEvent("GamePlay", "PlayGame", "", (int)Time.fixedTime);
		//MyApplication.Instance.googleAnalytics.LogScreen ("GamePLay");
        gameStart.SetActive(false);
        gamePlay.SetActive(true);
        _gameController = gamePlay.GetComponentInChildren<GameController>();
        _uiController = gamePlay.GetComponentInChildren<UIController>();
        ReadyGo.Instance.Test();
        AvtiveTutorial();

    }
    public void GameOver()//Game Over
    {

        ReadyGo.Instance.Reset();
#if UNITY_ANDROID || UNITY_IOS
        //Debug.Log("Show Banner");
		GoogleAds_J60s.Instance.HideBanner();
#endif
        MyApplication.Instance.googleAnalytics.LogEvent("GameOver", "Over", "", (int)Time.fixedTime);
		//MyApplication.Instance.googleAnalytics.LogScreen ("GameOver");
        gameOver.gameObject.SetActive(true);
        gameOverController = gameOver.GetComponent<GameOver>();
        if (gameOverController != null)
        {
            gameOverController.SaveScore();
            gameOverController.SetMeo();
            hightPointScore = gameOverController.GetHightScore();
            _uiController.ResetFillAmount();
         
        }
        
    }
    public void GameMenu()//Menu Game
    {
        ReadyGo.Instance.Reset();
#if UNITY_ANDROID || UNITY_IOS
        //Debug.Log("Show Banner");
        GoogleAds_J60s.Instance.ShowBanner();
#endif
        MyApplication.Instance.googleAnalytics.LogEvent("GameMenu", "JoinGame", "", (int)Time.fixedTime);
		//MyApplication.Instance.googleAnalytics.LogScreen ("GameMenu");
        if (_gameController != null && _uiController != null)
        {
            _uiController.GameRelay();
            _gameController.RandomMap();
            _uiController.ResetFillAmount(); 
            gameOver.txtMeo.text = "";
        }
        gameOver.gameObject.SetActive(false);
        gamePlay.SetActive(false);
        gameStart.SetActive(true);

        
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void GameRelay()
    {

#if UNITY_ANDROID || UNITY_IOS
        //Debug.Log("Show Banner");
		GoogleAds_J60s.Instance.HideBanner();
        
#endif
        MyApplication.Instance.googleAnalytics.LogEvent("GameRelay", "ReplayGame", "", (int)Time.fixedTime);
        //MyApplication.Instance.googleAnalytics.LogScreen("GameRelay");
        gameOver.gameObject.SetActive(false);
        gameOver.txtMeo.text = "";
        if (_gameController != null && _uiController != null)
        {
            _uiController.GameRelay();
            _gameController.RandomMap();

        }
        ReadyGo.Instance.Test();
        AvtiveTutorial();
    }
    public void HightScore()
    {
        gameStart.SetActive(false);
        hightScore.SetActive(true);
        txtHightScore.text = hightPointScore.ToString();
    }
    public void Back()
    {
        hightScore.SetActive(false);
        gameStart.SetActive(true);
    }
    int indexMute = 0;
    public void MuteMussic()
    {
        indexMute++;
        if (indexMute % 2 != 0)
        {
            audioGamePlay.mute = true;
        }
        else
        {
            audioGamePlay.mute = false;
        }
    }
    public void Rating()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.bluebirdaward.Fuky.Jelly60s");
    }
    void AvtiveTutorial()
    {
        finishTutorial.gameObject.SetActive(true);
        finishTutorial.ResetTutorial();
    }
}
