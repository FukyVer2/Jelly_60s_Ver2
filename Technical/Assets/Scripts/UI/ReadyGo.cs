using UnityEngine;
using System.Collections;

public class ReadyGo : MonoSingleton<ReadyGo>
{


    public GameObject txtBackground;
    public GameObject txtReady;
    public GameObject txtGo;


    private bool isBackground;
    private bool isReady;
    public bool isGo;

    public Vector3 posBackgroundStart;
    public Vector3 posReadyStart;
    public Vector3 posgGoStart;

	// Use this for initialization
	void Start () {
        posBackgroundStart = txtBackground.transform.position;
        posReadyStart = txtReady.transform.position;
        posgGoStart = txtGo.transform.position;
        //MoveItween(txtBackground, Vector3.zero, 1, iTween.EaseType.easeOutBack, "MoveBackGround");
	}
	
	// Update is called once per frame
	void Update () {
        UpdateITween();
	}
    [ContextMenu("Test")]
    public void Test()
    {
        Reset();
        MoveItween(txtBackground, Vector3.zero, 0.5f, iTween.EaseType.easeOutBack, "MoveBackGround");
        
    }
    void UpdateITween()
    {
        if (isBackground)
        {
            MoveItween(txtReady, Vector3.zero, 0.5f, iTween.EaseType.easeOutBack, "MoveReady");
        }
        if (isReady)
        {
            MoveItween(txtGo, Vector3.zero, 0.5f, iTween.EaseType.easeOutBack, "MoveGo");
        }
    }
    public void MoveItween(GameObject obj, Vector3 pos, float movetime, iTween.EaseType easeType, string ham)
    {
        iTween.MoveTo(obj, iTween.Hash(
            iT.MoveTo.position, pos,//toi vi tri cuoi
            iT.MoveTo.islocal, true,
            iT.MoveTo.time, movetime,//thoi gian
            iT.MoveTo.easetype, easeType,//hieu ung di chuyen
            iT.MoveTo.oncomplete, ham,//goi den ham Xoa
            iT.MoveTo.oncompletetarget, gameObject
            ));
    }
    void MoveBackGround()
    {
        isBackground = true;
    }
    void MoveReady()
    {
        txtReady.SetActive(false);
        isReady = true;
        txtBackground.transform.position = posBackgroundStart;
        txtReady.transform.position = posReadyStart;
        txtGo.transform.position = posgGoStart;
    }
    void MoveGo()
    {
        txtBackground.SetActive(false);
        txtGo.SetActive(false);
        isGo = true;
    }
    void Reset()
    {
        
        txtBackground.SetActive(true);
        txtReady.SetActive(true);
        txtGo.SetActive(true);
        isGo = false;
        
    }
}
