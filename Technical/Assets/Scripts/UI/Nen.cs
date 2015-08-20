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
	
	}
    void SetActive()
    {
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
                            listDoc[gem.collumn].SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
