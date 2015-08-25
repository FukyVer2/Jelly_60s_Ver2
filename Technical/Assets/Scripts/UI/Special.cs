using UnityEngine;
using System.Collections;

public class Special : MonoBehaviour {

    public GameObject left;
    public GameObject right;
    public bool isSpecial;
    public bool isRow;
    public float timeScale;
    public float speedRotaion;
    private float scale = 0.01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ScaleSpecial();
	}
    private float rotation = 0;
    void ScaleSpecial()
    {
        timeScale += scale;
        if (timeScale > 1.2)
        {
            scale = -0.01f;
        }
        if (timeScale < 0.8)
        {
            scale = 0.01f;
        }
        if (isSpecial)
        {
            rotation += speedRotaion * Time.deltaTime;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0,rotation );
        }
        else
        {
            if (isRow)
            {
                left.transform.localScale = new Vector3(-timeScale, 1, 1);
                right.transform.localScale = new Vector3(timeScale, 1, 1);
            }
            else
            {
                left.transform.localScale = new Vector3(timeScale, 1, 1);
                right.transform.localScale = new Vector3(timeScale, 1, 1);
            }
        }
    }
}
