using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreShareFb : MonoBehaviour {

    public GameOver gameOver;
    public Text txtScore;
    public Text yourScore;
    public Text bestScore;

	// Use this for initialization
	void Start () {
        SetText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetText()
    {
        int score = gameOver.GetScore();
        int heightScore = gameOver.GetHightScore();

        txtScore.text = score.ToString();
        yourScore.text = score.ToString();
        bestScore.text = heightScore.ToString();
    }
}
