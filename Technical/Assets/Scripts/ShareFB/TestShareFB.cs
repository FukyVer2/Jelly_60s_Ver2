using UnityEngine;
using System.Collections;

public class TestShareFB : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [ContextMenu("Share Image")]
    public void StartShareImage()
    {
        FB.Init(initFBDelegate, HideUnityDele, null);
    }
    public void initFBDelegate()
    {
        if (!FB.IsLoggedIn)
        {
            FB.Login("email,publish_actions", callBackLogin);

            //xin quyền xem thông tin email, quyền public - callBackLogin là callback           gọi sau khi login(có thể là thành công hoặc không thành công để xử lý)
        } 
    }
    public void HideUnityDele(bool isUnityHide)
    {
        if (!isUnityHide)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        // dùng để pause game khi đang cần đăng nhập

               
    }
    public void callBackLogin(FBResult result)
    {
        if (FB.IsLoggedIn)
        {
            //StartShareFB();
            StartCoroutine(TakeScreenshot1());
            //nếu login thành công gọi hàm TakeScreenshot để chụp màn                           hình dùng cho việc post facebook share ảnh
        }

        Debug.Log("Loging call back " + result.Text);
    }
    void PostPicCallback(FBResult result)
    {
        score.SetActive(false);
        if (result.Error != null)
        {
            Debug.LogWarning("FacebookManager-publishActionCallback: error: " + result.Error);
        }
        else
        {
            Debug.Log("FacebookManager-publishActionCallback: success: " + result.Text);
        }
    }
    private IEnumerator TakeScreenshot1()
    {
        score.SetActive(true);
        yield return new WaitForEndOfFrame();
        Texture2D snap = new Texture2D(Screen.width, Screen.height,
                TextureFormat.RGB24, false);

        Debug.Log("Da Share thanh Cong");
        //chup man hinh
#region
        //screenShotTexture.mainTexture = snap;
        //Texture hiển thị hình vừa chụp cho người dùng
        snap.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        snap.Apply();
        byte[] scren = snap.EncodeToPNG();
        //lấy chuỗi byte của hình để post lên facebook

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", scren, "barcrawling.png");


        wwwForm.AddField("message", "Ai vuot qua khong https://play.google.com/store/apps/details?id=com.Fuky.Jelly60s");

        FB.API("me/photos", Facebook.HttpMethod.POST, PostPicCallback, wwwForm);//post
#endregion
    }
    public void StartShareFB()
    {
        score.SetActive(true);
        StartCoroutine(DelaySceen());
    }
    public GameObject score;
    IEnumerator DelaySceen()
    {
        yield return new WaitForEndOfFrame();
        //Debug.Log("Delay Time");
        //bat dau chup man hinh
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        Rect r = new Rect(0, 0, (float)width, (float)height);
        tex.ReadPixels(r, 0, 0);

        //tex.LoadImage()

        tex.Apply();

        //byte[] screenshot = tex.EncodeToPNG();
        screenshot = tex.EncodeToPNG();


        StartCoroutine(DelaySceenShot());
    }
    byte[] screenshot;

    IEnumerator DelaySceenShot()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Delay Time Screen Shot");
        score.SetActive(false);
        UpdateShareFB();

    }
    void UpdateShareFB()
    {
        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "https://play.google.com/store/apps/details?id=com.Fuky.Jelly60s");

        FB.API("me/photos", Facebook.HttpMethod.POST, PostPicCallback, wwwForm);
    }

}
