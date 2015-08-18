using UnityEngine;
using System.Collections;

public class HieuUng : MonoBehaviour
{

    private float localScale = 0.5f;
    public float scale = 0.005f;

    public bool isSpecial;
    public bool isSpecialCollum;
    public bool isSpecialRow;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        localScale += scale;
        if (localScale >= 0.7f)
        {
            scale = -0.005f;
        }
        if (localScale <= 0.4f)
        {
            scale = 0.005f;
        }
        gameObject.transform.localScale = new Vector3(localScale, localScale, 1);
    }
    public int RenderSpecial()
    {
        int index = -1;
        if (isSpecial)
        {
            index = 2;
            return index;
        }
        if (isSpecialCollum)
        {
            index = 0;
            return index;
        }
        if (isSpecialRow)
        {
            index = 1;
            return index;
        }
        return index;
    }
    public void MoveItween(Vector3 pos, float movetime)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            iT.MoveTo.position, pos,//toi vi tri cuoi
            iT.MoveTo.islocal, true,
            iT.MoveTo.time, movetime,//thoi gian
            iT.MoveTo.oncomplete, "DestroyItween",
            iT.MoveTo.oncompletetarget, gameObject
            ));
    }
    public void DestroyItween()
    {
        Destroy(gameObject);
    }

}
