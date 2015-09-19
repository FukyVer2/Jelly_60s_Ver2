using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FBHolder : MonoBehaviour {
	//public GameObject UIFbIsLoggedIn, UIFbNotLoggedIn, UIFbAvatar, UIFbUserName;
	//public GameObject UIFbAvatar;
	//public GameObject UIFbUserName;
	//private Dictionary <string, string> profile = null;

	//public Text ScoresDebug;
	//private List<object> scoresList = null;
	// Use this for initialization
	void Start () {
		//UIFbIsLoggedIn = GameObject.Find ("FB Logged In");
		//UIFbNotLoggedIn = GameObject.Find ("FB Not Logged In");
		//UIFbAvatar = GameObject.Find ("FB Avatar");
		//UIFbUserName = GameObject.Find ("FB User Name");
		//ScoresDebug = GameObject.Find ("ScoresDebug");
	}	
	// Update is called once per frame
	//

	void Awake()
	{
		FB.Init (SetInit, onHideUnity);
	}

	private void SetInit()
	{
		print ("FB Init done");
		if (FB.IsLoggedIn) //logged in OK
		{
			//DealWithFBMenus(true);
			print ("FB is Logged in");
		}
		else
		{
			//DealWithFBMenus(false);
			//FBLogin();
		}
	}
	private void onHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}


	//public void FBLogin()
	void FBLogin()
	{
		FB.Login ("email,publish_actions", AuthCallBack);
	}
    public bool isShare = false;
	void AuthCallBack(FBResult result)
	{
		if (FB.IsLoggedIn)
		{
			//DealWithFBMenus(true);
			print("FB login worked");
            //LinkAndPicture();
            //TakeScreenshot();
            if (!isShare)
                //FBfeed();
                ShareFBScreenStart();
            else
                UpdateShareFB();
		}
		else
		{
			//DealWithFBMenus(false);
			print("FB login Failed");
		}
	}


	/*
	void DealWithFBMenus(bool IsLoggedIn)
	{
		if (IsLoggedIn)
		{
			//UIFbIsLoggedIn.SetActive(true);
			//UIFbNotLoggedIn.SetActive(false);
			//get avatar:
			FB.API (Util.GetPictureURL ("me",  128, 128), Facebook.HttpMethod.GET, DealWithAvatar);
			//get userNam:
			FB.API ("/me?fields=id,first_name",Facebook.HttpMethod.GET, DealWithUserName);
		}
		else
		{
			//UIFbIsLoggedIn.SetActive(false);
			///UIFbNotLoggedIn.SetActive(true);
		}
	}

	void DealWithAvatar(FBResult result)
	{
		if (result.Error != null)
		{
			print("problem with avatar");
			FB.API (Util.GetPictureURL ("me",  128, 128), Facebook.HttpMethod.GET, DealWithAvatar);
			return;
		}

		///Image UserAvatar = UIFbAvatar.GetComponent<Image> ();
		//UserAvatar.sprite = Sprite.Create(result.Texture, new Rect(0,0,128,128), new Vector2(0,0));
	}

	void DealWithUserName(FBResult result)
	{
		if (result.Error != null)
		{
			print("problem with name");
			FB.API ("/me?fields=id,first_name",Facebook.HttpMethod.GET, DealWithUserName);
			return;
		}

		//profile = Util.DeserializeJSONProfile (result.Text);
		//Text UserMsg = UIFbUserName.GetComponent<Text> ();
		//UserMsg.text = "Hello, " + profile ["first_name"];
	}
	*/
	/*
	public void ShareWithFriends()
	{
		FB.Feed (
			linkCaption: "I'm playing this awesome game",
			//picture: """http://robovina.com/uploads/news/2015_03/autobase1.png",
			linkName: "Check out this game",
			//link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "Guest")
			link: "http://robovina.com"
		);
	}
*/
	public void ShareWithFriends()
    {
        isShare = false;
	    //check login or not yet:
	    if (FB.IsLoggedIn) //logged in OK -> goto share to FB
	    {
			print ("logged in OK -> goto share to FB");
            //LinkAndPicture();
            //TakeScreenshot();
			//FBfeed();
            ShareFBScreenStart();
	    }
	    else //not login yet, so go to login and share
	    {
			print("not login yet, so go to login and share");
			FBLogin();
			//ShareWithFriends();
			//goto loop;
		    //DealWithFBMenus(false);
		    //FBLogin();
	    }
    }
	void FBfeed()
	{
		FB.Feed(
			linkCaption: "I'm playing this awesome game",
            picture: "",
			linkName: "Check out this game",
			//link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "Guest")
            link: "https://play.google.com/store/apps/details?id=com.Fuky.Jelly60s"
			//mediaSource: "https://www.youtube.com/watch?v=dAXW1R_aoz4"
			);
	}
    [ContextMenu("Create Imgae")]
    void TakeScreenshot()
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        tex.Apply();
        byte[] screenshot = tex.EncodeToPNG();

        var wwwForm = new WWWForm();

        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "https://play.google.com/store/apps/details?id=com.bluebirdaward.Fuky.Jelly60s");

        FB.API("me/photos", Facebook.HttpMethod.POST, PostPicCallback, wwwForm);
        //FB.API("/me?fields=first_name", Facebook.HttpMethod.POST, PostPicCallback, wwwForm);
        //FBfeed();
    }
    void LinkAndPicture()
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        tex.Apply();
        byte[] screenshot = tex.EncodeToPNG();

        var wwwForm = new WWWForm();

        string picName = "Idioman_" + Time.time + ".png";

        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "Game Hay Qua");

        FB.Feed(
            linkCaption: "I'm playing this awesome game",
            picture: "InteractiveConsole.png",
            linkName: "Check out this game",
            //link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "Guest")
            link: "https://play.google.com/store/apps/details?id=com.bluebirdaward.Fuky.Jelly60s",
            mediaSource: "https://www.youtube.com/watch?v=dAXW1R_aoz4"
            );
    }
    void PostPicCallback(FBResult result)
    {
        if (result.Error != null)
        {
            Debug.LogWarning("FacebookManager-publishActionCallback: error: " + result.Error);
        }
        else
        {
            Debug.Log("FacebookManager-publishActionCallback: success: " + result.Text);
        }
    }

    public void ShareFBScreenStart()
    {
        FB.Feed(
            linkCaption: "Chơi Game Nào",
            picture: "",
            linkName: "Jelly 60S",
            //link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "Guest")
            link: "https://play.google.com/store/apps/details?id=com.Fuky.Jelly60s"
            //mediaSource: "https://www.youtube.com/watch?v=dAXW1R_aoz4"
            );
    }
    public GameObject score;
    [ContextMenu("TestIEnumerator")]
    public void StartShareFB()
    {
        isShare = true;
        score.SetActive(true);
        StartCoroutine(DelaySceen());
    }
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
        if (FB.IsLoggedIn) //logged in OK -> goto share to FB
        {
            print("logged in OK -> goto share to FB");
            UpdateShareFB();
        }
        else //not login yet, so go to login and share
        {
            print("not login yet, so go to login and share");
            FBLogin();

        }

    }
    void UpdateShareFB()
    {
        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "https://play.google.com/store/apps/details?id=com.Fuky.Jelly60s");

        FB.API("me/photos", Facebook.HttpMethod.POST, PostPicCallback, wwwForm);
    }

	/*
	public void InviteFriends()
	{
		FB.AppRequest (
			message: "This game is awesome, play now with me",
			title: "Invite your friends to play"
		);
	}
	*/
	/*
	public void QueryScores()
	{
		FB.API ("/app/scores?fields=score,user.limit(30)", Facebook.HttpMethod.GET, ScoresCallBack);
	}
	private void ScoresCallBack(FBResult result)
	{
		print ("scores callback" + result.Text);
		//ScoresDebug.text = result.Text;
		//ScoresDebug.text = "";

		scoresList = Util.DeserializeScores (result.Text);

		foreach (object score in scoresList)
		{
			var entry = (Dictionary <string, object>) score;
			var user = (Dictionary <string, object>) entry["user"];

			ScoresDebug.text = ScoresDebug.text+ "UN: " + user["name"] + " - " + entry["score"] + ", " ;
		}
	}

	public void SetScores()
	{
		var scoreData = new Dictionary <string, string> ();
		scoreData ["score"] = Random.Range (10, 200).ToString ();
		FB.API ("/me/scores", Facebook.HttpMethod.POST, delegate (FBResult result) {
			print ("Scores submit result: " + result.Text);
		}, scoreData);
	}
	*/

}

