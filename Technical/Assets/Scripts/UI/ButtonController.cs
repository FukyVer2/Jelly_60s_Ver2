﻿using UnityEngine;
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
#if UNITY_ANDROID
        ChartboostAndroid.Instance.RequestInterstitial(ChartboostSDK.CBLocation.Default);
        ChartboostAndroid.Instance.RequestRewardedVideo(ChartboostSDK.CBLocation.Default);
#endif
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
        //GoogleAds_J60s.Instance.ShowBanner();
#endif
        MyApplication.Instance.googleAnalytics.LogEvent("GamePlay", "PlayGame", "", (int)Time.fixedTime);
		//MyApplication.Instance.googleAnalytics.LogScreen ("GamePLay");
        gameStart.SetActive(false);
        gamePlay.SetActive(true);
        _gameController = gamePlay.GetComponentInChildren<GameController>();
        _uiController = gamePlay.GetComponentInChildren<UIController>();
        ReadyGo.Instance.Test();
        AvtiveTutorial();
        AudioController.Instance.PlaySound(AudioType.BUTTON_CLICK);

    }
    public void GameOver()//Game Over
    {

        ReadyGo.Instance.Reset();
#if UNITY_ANDROID || UNITY_IOS
        //Debug.Log("Show Banner");
		//GoogleAds_J60s.Instance.HideBanner();
        //ChartboostAndroid.Instance.ShowInterstitial(ChartboostSDK.CBLocation.Default);
        //StartCoroutine(ShowChartbook());
        Invoke("ShowChartbook", 0.5f);
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
    void ShowChartbook()
    {
        Debug.Log("Show Chartbook");
        ChartboostAndroid.Instance.ShowInterstitial(ChartboostSDK.CBLocation.Default);
    }
    public void GameMenu()//Menu Game
    {
        ReadyGo.Instance.Reset();
        FinishTutorial.Instance.Finish();
#if UNITY_ANDROID || UNITY_IOS
        //Debug.Log("Show Banner");
        //GoogleAds_J60s.Instance.ShowBanner();
#endif
        MyApplication.Instance.googleAnalytics.LogEvent("GameMenu", "JoinGame", "", (int)Time.fixedTime);
		//MyApplication.Instance.googleAnalytics.LogScreen ("GameMenu");
        if (_gameController != null && _uiController != null)
        {
            _uiController.GameRelay();
            _gameController.RandomMap();
            _uiController.  ResetFillAmount(); 
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
		//GoogleAds_J60s.Instance.HideBanner();
        
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
        AudioController.Instance.PlaySound(AudioType.BUTTON_CLICK);
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
    public Image btMuteMussic;
    public Image btMuteMussic1;
    public Sprite spriteMussic;
    public Sprite spriteMuteMussic;
    public void MuteMussic()
    {
        indexMute++;
        if (indexMute % 2 != 0)
        {
            
            btMuteMussic.sprite = spriteMuteMussic;
            btMuteMussic1.sprite = spriteMuteMussic;
            audioGamePlay.mute = true;
        }
        else
        {
            btMuteMussic.sprite = spriteMussic;
            btMuteMussic1.sprite = spriteMussic;
            audioGamePlay.mute = false;
        }
    }
    public void Rating()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Fuky.Jelly60s");
    }
    void AvtiveTutorial()
    {
        finishTutorial.gameObject.SetActive(true);
        finishTutorial.ResetTutorial();
    }
}
