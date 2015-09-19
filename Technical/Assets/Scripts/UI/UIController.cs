using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class UIController : MonoBehaviour
{

    public int countDelete;
    public int[] totalDelete;
    public GameObject[] list;
    public GameController gameController;
    public GameObject objTotalGem;
    public GameObject timeImgage;
    public GameObject pause;
    public List<GameObject> combo;
    public Text txtCountGem;
    public Text txtScore;
    public float timeGame = 60;
    public Text textTimeSecond;
    private float timeDelay = 0;

    public ButtonController buttonController;
    private float timeGameIT;
    private int scoreIT;
    private bool isPause;
    public bool isGameOver;
    private Image imageTimeGreen;
    private float fillTimeGreen = 1;

    // Use this for initialization
    void Start()
    {
        imageTimeGreen = timeImgage.GetComponent<Image>();
        isGameOver = false;
        isPause = false;
        countDelete = 0;
        totalDelete = new int[6];
        timeGameIT = timeGame;
        if (gameController != null)
        {
            scoreIT = gameController.score;
        }
        fillAmount = 1;
    }

    Color32 c = Color.red;
    // Update is called once per frame
    void Update()
    {
        if (FinishTutorial.Instance.isStart)
        {
            if (isPause == false)
            {
                if (timeDelay > 1 && timeGame > 0)
                {
                    
                    timeGame -= 1;//sau 1s thời gian giảm xuống
                    if (timeGame <= 0)// nếu < 0 sẽ xuất hiện màn hình Game Over
                    {
                        isGameOver = true;
                        GameOver();
                        gameController.ExpScore();
                    }
                    timeDelay = 0;
                    
                }
                if (timeGame != timeGameIT)
                {
                    Scale("ChangleScaleTime");//hiệu ứng to nhỏ thời gian
                    timeGameIT = timeGame;
                }
                if (gameController != null)
                {
                    if (gameController.score != scoreIT)
                    {
                        Scale("ChangleScaleScore");//hiệu ứng to nhỏ điểm
                        scoreIT = gameController.score;
                    }
                }
                timeDelay += Time.deltaTime;
                ChangleFillAmount();
                UpdateTime();
            }
            if (isCombo)
            {
                if (timeShowCombo <= 0)
                {
                    isCombo = false;
                    isPause = false;
                    combo[countCombo].SetActive(false);
                    timeShowCombo = 2;

                }
                timeShowCombo -= Time.deltaTime;
            }
           
        }
        TimeDanger();
    }
    public void AddTime(int _timeAdd)
    {
        timeGame += _timeAdd;
        if (timeGame > 60)
        {
            timeGame = 60;
        }
    }
    void UpdateTime()
    {
        SetText();
        UpdateImageTime();
        //timeImgage.transform.localScale = new Vector3((timeGame * 0.0166f), 1f, 1f);
        //timeImgage.GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, 1 - (timeGame * 0.0166f));
    }

    void UpdateImageTime()
    {
        imageTimeGreen.fillAmount = timeGame / 60.0f;
    }
    public void SetText()
    {
        textTimeSecond.text = timeGame.ToString();
        //txtCountGem.text = countDelete.ToString();
        //buttonPause.GetComponentInChildren<Text>().text = "Pause";
        txtScore.text = System.String.Format("{0}", gameController.score);
    }

    public int timeAddValue = 0;
    float fillAmount = 1;
    public void ChangleFillAmount()
    {
        fillAmount = (1 - countDelete * 0.1f);
        if (fillAmount <= 0)
        {
            if (countDelete >= 10 && countDelete <= 12)
            {
                timeAddValue = 10;
            }
            if (countDelete > 12 && countDelete <= 15)
            {
                timeAddValue = 15;

            }
            if (countDelete > 15 && countDelete <= 20)
            {
                timeAddValue = 22;

            }
            if (countDelete > 20)
            {

                timeAddValue = 35;
            }
            gameController.activeAddtime = true;

        }
    }
    public void CheckCombo()
    {

        if (countGem >= 10)
        {
            countCombo += 1;
        }
        if (countGem < 10)
        {
            countCombo = 0;
        }
        List<Gem> listGem = gameController.listGemAddTime;
        if (listGem != null)
        {
            for (int i = 0; i < gameController.listGemAddTime.Count; i++)
            {
                if (listGem[i].addTimeGame > 4)
                {
                    listGem[i].addTimeGame -= 1;
                }
                for (int j = 0; j < listGem[i].gameObject.transform.GetChildCount(); j++)
                {
                    GameObject tObj = listGem[i].transform.GetChild(j).gameObject;
                    if (tObj != null)
                    {
                        TimeAdd t = tObj.GetComponent<TimeAdd>();
                        if (t != null)
                        {
                            t.textTime.text = listGem[i].addTimeGame.ToString();
                        }
                    }
                }
            }
        }
        //Debug.Log("Count Combo = " + countCombo);
    }
    public int countGem = 0;
    public int countCombo = 0;
    public bool x2Score = false;
    private bool isCombo = false;
    private float timeShowCombo = 2;
    public void Combo()
    {
        if (countCombo >= 2)
        {
            isPause = true;
            x2Score = true;
            //Debug.Log("COMBO X2");
            combo[countCombo].SetActive(true);
            isCombo = true;
        }
    }


    public void ResetFillAmount()
    {
        fillAmount = 1;
        imageTimeGreen.fillAmount = 1;
        //timeImgage.transform.localScale = new Vector3(1.0f, 1f, 1f);
    }
    public void RandomSpecial(int count, int i)
    {
        if (totalDelete[i] > 15)
        {
            int rand = UnityEngine.Random.Range(0, 100);

            if (rand < 20)
            {
                gameController.indexRandom = 2;
            }
            if (rand > 20 && rand < 60)
            {
                gameController.indexRandom = 1;
            }
            if (rand > 60)
            {
                gameController.indexRandom = 0;
            }
            totalDelete[i] = 0;

        }
    }
    public void GameOver()
    {
        imageTimeGreen.color = Color.white;
        ReadyGo.Instance.Reset();
        buttonController.GameOver();
        
    }
    public void GameRelay()
    {
        timeGame = 60;
        intPause = 0;
        countCombo = 0;
        //combo.SetActive(false);
        for (int i = 0; i < combo.Count;i++ )
        {
            combo[i].SetActive(false);
        }
        isCombo = false;
        x2Score = false;
        isPause = false;
        isGameOver = false;
        timeGameIT = timeGame;
        totalDelete = new int[6];
        ResetFillAmount();
        if (gameController != null)
        {
            scoreIT = gameController.score;
        }
        countDelete = 0;
        timeDelay = 0;
        SetText();
        pause.SetActive(false);
        
    }

    void Scale(String name)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
                   iT.ValueTo.from, 0f,
                   iT.ValueTo.to, 1f,
                   iT.ValueTo.time, 0.5f,
                   iT.ValueTo.onupdate, name,
                   iT.MoveTo.oncompletetarget, gameObject
                   ));

    }
    void ChangleScaleTime(float percent)
    {
        textTimeSecond.gameObject.transform.localScale = new Vector3(
            1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1
            );
    }
    void ChangleScaleScore(float percent)
    {
        txtScore.gameObject.transform.localScale = new Vector3(
            1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1 + (0.5f - Mathf.Abs(percent - 0.5f)) * (2f - 1),
             1
            );
    }
    int intPause = 0;
    public void Pause()
    {
        FinishTutorial.Instance.Finish();
        gameController.activeTimeHelp = false;
        //buttonPause.GetComponentInChildren<Text>().text = "Resume";
        isPause = true;
        pause.SetActive(true);

        
       
    }
    public void Resumes()
    {     
        gameController.activeTimeHelp = true;
        //buttonPause.GetComponentInChildren<Text>().text = "Pause";
        isPause = false;
        pause.SetActive(false);        
    }

    float x = 0;
    float y = 0;
    void TimeDanger()
    {
        if(timeGame <= 10)
        {
            x += Time.deltaTime;
            if(x >= 0.5f)
            {
                y += 1;
                if(y % 2 ==0)
                {
                    imageTimeGreen.color = Color.white;
                }
                else
                {
                    imageTimeGreen.color = Color.red;
                }          
                x = 0;
            }
           
        }
        else
        {
            imageTimeGreen.color = Color.white;
        }
    }
   
}
