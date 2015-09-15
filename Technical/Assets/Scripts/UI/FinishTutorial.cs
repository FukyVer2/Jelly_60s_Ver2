using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FinishTutorial : MonoSingleton<FinishTutorial>, IPointerDownHandler
{
    public Tutorial tutorialObj;
    public bool isStart = false;
    public bool jelly;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
       	}
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
       
    }
    public void Finish()
    {
        if (jelly && isStart == false)
        {
            isStart = true;
            tutorialObj.gameObject.SetActive(false);
            //gameObject.SetActive(false);
        }
    }
    public void Jelly()
    {
        jelly = true;
        tutorialObj.gameObject.SetActive(true);
        tutorialObj.TestTutorial();        
    }
    public void ResetTutorial()
    {
        jelly = false;
        isStart = false;
        tutorialObj.gameObject.SetActive(false);
    }
}
