using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    List<string> listTxtMeo = new List<string>();

    public Text txtMeo;

    public GameController gameController;
    public Text yourScore;
    public Text hightScore;


    // Use this for initialization
    void Start()
    {
        SaveScore();
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    //lưu Hight Score cho người chơi
    public void SaveScore()
    {
        
        if (gameController != null)
        {

            if (gameController.score > PlayerPrefs.GetInt("Score"))//kiểm tra điểm người chơi có cao hơn HightScore
            {
                PlayerPrefs.SetInt("Score", gameController.score);
                PlayerPrefs.Save();//Lưu Hight Score
				MyApplication.Instance.googleAnalytics.LogEvent("HighScore", "HightScore", "" + gameController.score, (int)Time.fixedTime);
			}else {
				MyApplication.Instance.googleAnalytics.LogEvent("NoScore", "NoScore", "" +gameController.score, (int)Time.fixedTime);
			}
        }
        SetText();
    }
    public void SetText()
    {
        if (gameController != null)
        {
            yourScore.text = gameController.score.ToString();
        }
        hightScore.text = PlayerPrefs.GetInt("Score").ToString();
        
    }
    public int GetHightScore()
    {
        int _hightScore = PlayerPrefs.GetInt("Score");
        return _hightScore;
    }
    public int GetScore()
    {
        int _score = gameController.score;
        return _score;
    }

    
    public void SetMeo()
    {
        listTxtMeo.Add("Item add time appears when pull more than 10 jelly");
        listTxtMeo.Add("Pull more jelly, add more score");
        listTxtMeo.Add("Multiply 2 or multiply 3 score when to have pull two or three times than 10 jelly in succession");
        int index = Random.Range(0, listTxtMeo.Count - 1);
        txtMeo.text = listTxtMeo[index];
    }

}
