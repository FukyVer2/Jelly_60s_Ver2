using UnityEngine;
using System.Collections;

public class TestCreateTexture : MonoBehaviour {

    public Texture tex;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [ContextMenu("TestCreateTexture")]
    void CreateTexture()
    {
        Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        //texture.SetPixel(0, 0, Color.Lerp(1.0f, 1.0f, 1.0f));
        texture.SetPixel(1, 0, Color.clear);
        texture.SetPixel(0, 1, Color.white);
        texture.SetPixel(1, 1, Color.black);

        texture.Apply();
        GetComponent<SpriteRenderer>().material.mainTexture = texture;
    }
}
