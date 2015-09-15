using UnityEngine;
using System.Collections;

public class TestShareFB : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FB.Init(initFBDelegate, HideUnityDele, null);
	}
	
	// Update is called once per frame
	void Update () {
	
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
            StartCoroutine(TakeScreenshot1());
            //nếu login thành công gọi hàm TakeScreenshot để chụp màn                           hình dùng cho việc post facebook share ảnh
        }

        Debug.Log("Loging call back " + result.Text);
    }
    private IEnumerator TakeScreenshot1()
    {
        yield return new WaitForSeconds(2f);
        Texture2D snap = new Texture2D(Screen.width, Screen.height,
                TextureFormat.RGB24, false);

        Debug.Log("Da Share thanh Cong");
        //chup man hinh
#region
    //    screenShotTexture.mainTexture = snap;
    //    //Texture hiển thị hình vừa chụp cho người dùng
    //    snap.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //    snap.Apply();
    //    byte[] scren = snap.EncodeToPNG();
    //    //lấy chuỗi byte của hình để post lên facebook

    //    var wwwForm = new WWWForm();
    //    wwwForm.AddBinaryData("image", screenshot, "barcrawling.png");


    //    wwwForm.AddField("name", message);
        //    FB.API("me/photos", HttpMethod.POST, callBackPostPhoto, wwwForm);//post
#endregion
    }

}
