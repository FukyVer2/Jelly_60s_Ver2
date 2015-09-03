using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour {

    public UIController uiController;
    public Image top;
    public Image down;
    public float deltaMin;
    public float deltaMax;
    public float value = 1;


    private float delta;
    private Color colorTop;
    private Color colorDown;


	// Use this for initialization
	void Start () {
        colorTop = top.color;
        colorDown = down.color;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateDanger();
	}
    float t = 0;
    void UpdateDanger()
    {
        Color c = top.color;
        c.a += value;
        if (c.a > deltaMax)
        {
            value *= -1;
        }
        if (c.a < deltaMin)
        {
            value *= -1;
        }
        top.color = c;
        down.color = c;
    }
}
