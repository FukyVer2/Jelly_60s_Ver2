using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NumberDrag : MonoBehaviour {


    public int number;
    public SpriteRenderer head;
    public SpriteRenderer tail;
    public List<Sprite> listNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [ContextMenu("Test")]
    public void Calculogic()
    {
        int hangChuc = -1;
        int hangDonVi = -1;
        if(number >= 10)
        {
            hangChuc = number / 10;
            head.transform.localPosition = new Vector3(-0.1f, 0, 0);
            tail.transform.localPosition = new Vector3(0.07f, 0, 0);
        }
        
        hangDonVi = number % 10;
        if (number >= 10)
        {
            if (hangChuc != -1)
            {
                head.sprite = listNumber[hangChuc];
            }
        }
        if (number < 10)
        {
            head.sprite = null;
        }
        if(hangDonVi != -1)
        {
            tail.sprite = listNumber[hangDonVi];
        }
        Scale();
    }
    [ContextMenu("ScaleIT")]
    void Scale()
    {
        iTween.ScaleTo(gameObject, iTween.Hash(
            iT.ScaleTo.scale,new Vector3(0.2f,0.2f,0),
            iT.ScaleTo.delay,0.2f,
            iT.ScaleTo.time , 0.7f,
            iT.ScaleTo.easetype, iTween.EaseType.linear,
            iT.MoveTo.oncomplete, "Finish",
            iT.MoveTo.oncompletetarget, gameObject

            ));
    }
    void Finish()
    {
        Destroy(gameObject);
    }
}
