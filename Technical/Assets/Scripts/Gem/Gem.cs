using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gem : MonoBehaviour
{

    public Sprite spriteChange;
    public Sprite spriteStart;
    // Use this for initialization
    public int collumn;
    public int row;
    public int inDex;
    public bool check;
    public bool destroyCollum = false;
    public bool destroyRow = false;
    public bool cucDacBiet = false;
    public bool timeAdd = false;
    public int disX;
    public int disY;
    private GameObject a;

    public float GemHeight;
    public float GemWitdh;
    public int countChild;

    Sprite start;
    Sprite changle;
    int indexStart;
    float timeSpringy;
    int timeRd;
    public bool isCollumn;
    public bool isRow;

    public bool isSpecial;
    public bool isSpecialRow;
    public bool isSpecialCollum;

    public int addTimeGame = 0;

    public Gem()
    {
        this.collumn = 0;
        this.row = 0;
        this.inDex = 0;
    }

    void Start()
    {
        start = gameObject.GetComponent<Image>().sprite;
        gameObject.GetComponent<Image>().sprite = spriteStart;
        indexStart = inDex;
        changle = spriteChange;
        ResetSprite();
        posY = gameObject.transform.localPosition.y;


    }
    // Update is called once per frame
    void Update()
    {
        ChildCount();
        GetChil();
    }
    //kiểm tra các con của Gem
    public void ChildCount()
    {
        if (destroyCollum == true || destroyRow == true)
        {
            countChild = 1;

        }
        if (destroyCollum == true && destroyRow == true)
        {
            countChild = 2;

        }
    }
    //Set thông tin của Gem: 
    //hàng, cột, loại Gem
    public void SetProfile(int col, int row, int index)
    {
        this.collumn = col;
        this.row = row;
        this.inDex = index;
    }
    //Đổi Sprite khi kéo
    public void ChangSprite()
    {
        gameObject.GetComponent<Image>().sprite = spriteChange;
    }
    //Đổi Sprite về trạng thái ban đầu
    public void ResetSprite()
    {
        gameObject.GetComponent<Image>().sprite = spriteStart;

    }
    public void ResetSpriteStart()
    {
        start = spriteStart;
        changle = spriteChange;
        indexStart = inDex;
    }
    public void Test(GameObject obj)
    {
        spriteStart = obj.GetComponent<Gem>().spriteStart;
        spriteChange = obj.GetComponent<Gem>().spriteChange;
        inDex = obj.GetComponent<Gem>().inDex;
        start = spriteStart;
        changle = spriteChange;
        indexStart = inDex;

    }
    //Đổi Sprite thành màu cùng cục đặc biệt
    public void ChangSpriteDacBiet(GameObject obj)
    {
        if (cucDacBiet == false)
        {
            a = obj;
            inDex = obj.GetComponent<Gem>().inDex;
            gameObject.GetComponent<Image>().sprite = obj.GetComponent<Gem>().spriteStart;
            spriteChange = obj.GetComponent<Gem>().spriteChange;
            spriteStart = obj.GetComponent<Gem>().spriteStart;

        }

    }
    //Reset lại Sprite
    public void ResetSpriteDacBiet(GameObject obj)
    {
        if (cucDacBiet == false)
        {
            inDex = indexStart;
            gameObject.GetComponent<Image>().sprite = start;
            spriteStart = start;
            spriteChange = changle;
        }

    }
    public void Reset()
    {
        isSpecial = false;
        isSpecialCollum = false;
        isSpecialRow = false;
    }
    public void ResetActive()
    {
        destroyCollum = false;
        destroyRow = false;
        cucDacBiet = false;
        timeAdd = false;

        listChil.Clear();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void SetSprite()
    {
        if (a == null)
        {
            Debug.Log("chua vao");
            return;
        }
        else
        {
            spriteChange = a.GetComponent<Gem>().spriteChange;
            spriteStart = a.GetComponent<Gem>().spriteStart;
        }
    }

    public int PosX()
    {
        int posX = (int)((gameObject.transform.localPosition.x) / (GemWitdh + disX) + 3.0f);
        return posX;
    }
    public int PosY()
    {
        int posY = (int)((gameObject.transform.localPosition.y + GemHeight / 2f + disY) / (GemHeight + disY) + 3.5f);
        return posY;
    }
    public void MovePositionStar(Vector3 pos, float movetime)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            iT.MoveTo.position, pos,//toi vi tri cuoi
            iT.MoveTo.islocal, true,
            iT.MoveTo.time, movetime

            ));
    }

    public void MovePosition(Vector3 pos, float movetime)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            iT.MoveTo.position, pos,//toi vi tri cuoi
            iT.MoveTo.islocal, true,
            iT.MoveTo.time, movetime,//thoi gian
            iT.MoveTo.easetype, iTween.EaseType.easeOutBack,//hieu ung di chuyen
            iT.MoveTo.oncomplete, "ChangleScale",
            iT.MoveTo.oncompletetarget, gameObject
            ));
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

    void UpdateScale(float percent)
    {
        gameObject.transform.localScale = new Vector3(
            1 + (0.5f - Math.Abs(percent - 0.5f)) * (1.2f - 1),
             1 + (0.5f - Math.Abs(percent - 0.5f)) * (1.2f - 1),
             1
            );
    }
    [ContextMenu("SpringyIT")]
    void SpringyIT()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
                   iT.ValueTo.from, 10f,
                   iT.ValueTo.to, 0f,
                   iT.ValueTo.time, 0.5f,
                   iT.ValueTo.onupdate, "Springy",
                   iT.MoveTo.oncompletetarget, gameObject
                   ));

    }

    float posY;

    void Springy(float percent)
    {
        gameObject.transform.localPosition = new Vector3(
            transform.localPosition.x,
            posY + ((5f - Math.Abs(percent - 5f)) * (13f - 10)),
            0
            );
    }
    void SpringyWidthHight(float percent)
    {

    }
    public List<GameObject> listChil;
    public void GetChil()
    {
        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).gameObject.tag != "addtime" && gameObject.transform.GetChild(i).gameObject.tag != "special")
                {
                    if (!listChil.Contains(gameObject.transform.GetChild(i).gameObject))
                    {
                        listChil.Add(gameObject.transform.GetChild(i).gameObject);
                    }
                }

            }
        }
    }
}
