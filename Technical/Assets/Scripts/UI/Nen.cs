using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nen : MonoBehaviour {

    public GameController gameController;

    public GameObject ngangPrefabs;
    public GameObject docPrefabs;
    public List<GameObject> listNgang;
    public List<GameObject> listDoc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SetActive ();
	}
    void SetActive()
    {
		for (int i =0; i<listDoc.Count; i++) 
		{
			listDoc[i].SetActive(false);
		}
		for (int j = 0; j< listNgang.Count; j++)
		{
			listNgang[j].SetActive(false);
		}
        if(gameController != null)
        {
            List<GameObject> listDelete = gameController.listDeleteNen;
            if(listDelete != null)
            {
                for(int i =0;i <listDelete.Count;i++)
                {
                    Gem gem = listDelete[i].GetComponent<Gem>();
                    if(gem != null)
                    {
                        if(gem.destroyCollum)
                        {
                            listDoc[gem.row].SetActive(true);
                        }
						if(gem.destroyRow)
						{
							listNgang[6 - gem.collumn].SetActive(true);
						}
                    }
                }
            }
        }
    }
    public List<GameObject> list;

    public int collum;
    public int row;
    [ContextMenu("Test")]
    void Test()
    {
        Addlist(collum, row);
        Active(list);
    }
    void Addlist(int collum, int row)
    {
        for(int i =0;i <gameController.countCollumn;i++)
        {
            if (!list.Contains(gameController.arrGem[i][collum]))
                list.Add(gameController.arrGem[i][collum]);
        }
        for(int j =0;j<gameController.countRow;j++)
        {
            if (!list.Contains(gameController.arrGem[row][j]))
                list.Add(gameController.arrGem[row][j]);
        }
    }
    void Active(List<GameObject> listDelete)
    {
        for (int i = 0; i < listDoc.Count; i++)
        {
            listDoc[i].SetActive(false);
        }
        for (int j = 0; j < listNgang.Count; j++)
        {
            listNgang[j].SetActive(false);
        }
        if (gameController != null)
        {
            //List<GameObject> listDelete = gameController.listDeleteNen;
            if (listDelete != null)
            {
                for (int i = 0; i < listDelete.Count; i++)
                {
                    
                    Gem gem = listDelete[i].GetComponent<Gem>();
                    if (gem != null)
                    {
                        if (gem.destroyCollum)
                        {
                            listDoc[gem.row].SetActive(true);
                            Debug.Log("Set Active Row = " + gem.row);
                        }
                        if (gem.destroyRow)
                        {
                            listNgang[6 - gem.collumn].SetActive(true);
                            int z = 6 - gem.collumn;
                            Debug.Log("Set Active Collum = " + z);
                        }

                    }
                }
            }
        }
    }
}
