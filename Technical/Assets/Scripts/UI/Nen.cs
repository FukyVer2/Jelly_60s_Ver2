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
            List<GameObject> listDelete = gameController.ListDelete;
            if(listDelete != null)
            {
                for(int i =0;i <listDelete.Count;i++)
                {
                    Gem gem = listDelete[i].GetComponent<Gem>();
                    if(gem != null)
                    {
                        if(gem.destroyCollum)
                        {
							Debug.Log("Vi tri cuc dac biet la");
                            listDoc[gem.row].SetActive(true);
                        }
						if(!gem.destroyCollum)
						{
							listDoc[gem.row].SetActive(false);
						}
						if(gem.destroyRow)
						{
							listNgang[6 - gem.collumn].SetActive(true);
						}
						if(!gem.destroyRow)
						{
							listNgang[6 - gem.collumn].SetActive(false);
						}
                    }
                }
            }
        }
    }
}
