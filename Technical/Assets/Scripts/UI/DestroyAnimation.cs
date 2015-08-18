using UnityEngine;
using System.Collections;

public class DestroyAnimation : MonoBehaviour
{

    GameController gameControl;
    void Start()
    {
        GameObject obj = GameObject.Find("Canvas");
        gameControl = obj.GetComponentInChildren<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DestroyGem()
    {
        Destroy(gameObject);
        gameControl.activeDestroyGem = true;
    }
}
