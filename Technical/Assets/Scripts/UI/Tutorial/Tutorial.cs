﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour {

    public Vector3 posStart;
    public Vector3 posMove;
    public Vector3 posEnd;

    private Sprite sprite;
	// Use this for initialization
	void Start () {
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePos();
	}

    void UpdateTutorial(Vector3 pos)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            iT.MoveTo.position, pos,
            iT.MoveTo.time, 0.7f,
            iT.MoveTo.easetype, iTween.EaseType.linear,
            iT.MoveTo.oncomplete, "Test",
            iT.MoveTo.oncompletetarget, gameObject
            ));
        

    }
    void Test()
    {
        count++;
        if (count > 1)
        {
            TestTutorial();
        }
    }
    public int count = -1;
    [ContextMenu("Move")]
    void UpdatePos()
    {
        //if(count == 0)
        //{
        //    UpdateTutorial(posStart);
        //}
        if(count == 0)
        {
            UpdateTutorial(posMove);
        }
        if(count == 1)
        {
            UpdateTutorial(posEnd);
        }
        
    }

    [ContextMenu("TestTutorial")]
    public void TestTutorial()
    {
        Vector3 posMin = Vector3.zero;
        Vector3 posBetween = Vector3.zero;
        Vector3 posMax = Vector3.zero;

        List<List<GameObject>> listLoangDau = GameController.Instance.listLoangDau;
        List<GameObject> listGem = listLoangDau[0];
       
        if (GetGem(listGem[1]).collumn == GetGem(listGem[2]).collumn && GetGem(listGem[1]).row < GetGem(listGem[0]).row && GetGem(listGem[2]).row > GetGem(listGem[0]).row)
        {
            posMin = listGem[1].transform.position;
            posBetween = listGem[0].transform.position;
            posMax = listGem[2].transform.position;
        }
        else
        {
            for (int i = 0; i < 3 - 1; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    Swap(listGem[i], listGem[j]);
                }
            }
            posMin = listGem[0].transform.position;
            posBetween = listGem[1].transform.position;
            posMax = listGem[2].transform.position;
        }
        if (GetGem(listGem[0]).collumn == GetGem(listGem[1]).collumn && GetGem(listGem[0]).collumn < GetGem(listGem[2]).collumn && GetGem(listGem[2]).row < GetGem(listGem[0]).row)
        {
            posMin = listGem[2].transform.position;
            posBetween = listGem[0].transform.position;
            posMax = listGem[1].transform.position;
        }
        float width = 0.3f;
        float height = 0.525f;

        posStart = new Vector3(posMin.x + width, posMin.y + height, -1);
        gameObject.transform.position = posStart;

        posMove = new Vector3(posBetween.x + width, posBetween.y + height, -1);

        posEnd = new Vector3(posMax.x + width, posMax.y + height, -1);
        count = 0;
    }
    Gem GetGem(GameObject a)
    {
        return a.GetComponent<Gem>();
    }
    void Swap(GameObject a, GameObject b)
    {
        GameObject temp;
        Gem aGem = a.GetComponent<Gem>();
        Gem bGem = b.GetComponent<Gem>();
        if(aGem.collumn > bGem.collumn || aGem.row > bGem.row)
        {
            temp = a;
            a = b;
            b = temp;
        }
    }
    [ContextMenu("Size")]
    void Size()
    {
        float width = sprite.bounds.size.x / 2.0f - 0.2f;
        float height = sprite.bounds.size.y / 2.0f - 0.2f;
        Debug.Log("Width = " + width + "---------- Height = " + height);
    }
}
