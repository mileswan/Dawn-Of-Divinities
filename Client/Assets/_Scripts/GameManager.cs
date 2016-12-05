using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
/**
 * @author mwan
 * Version: 1.0
 * Date: 2016/12/5
 * Description: 主要管理类.
 */
public class GameManager : MonoBehaviour {

    public string loginUrl = "http://192.168.1.162:8088/OEPT_eSales/auth/login.do";
    public InputField inputUsername;
    public InputField inputPassword;

    private string menuItemSelected;
    public string menuItem { get { return menuItemSelected; } set { /* Debug.Log(value); */ menuItemSelected = value; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //检测键盘事件
    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log("Current Key is : " + KeyCode.Return.ToString());
            OnLogin();
        }
    }
    //退出程序
    public void OnExit()
    {
        Application.Quit();
    }
    //登录进入主菜单界面
    public void OnLogin()
    {
        StartCoroutine(BeginLogin());
    }
    private IEnumerator BeginLogin()
    {
        WWWForm form = new WWWForm();
        // Get username from input field
        form.AddField("username", inputUsername.text);
        // Get md5 encrypted password from input field
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] passwordData = System.Text.Encoding.Unicode.GetBytes(inputPassword.text);
        string str = System.Text.Encoding.Default.GetString(md5.ComputeHash(passwordData)) + inputUsername.text;
        byte[] fromData = System.Text.Encoding.Unicode.GetBytes(str);
        form.AddField("password", System.Text.Encoding.Default.GetString(md5.ComputeHash(fromData)));

        WWW response = new WWW(loginUrl, form);
        
        // Wait until the response is done
        yield return response;

        if (!string.IsNullOrEmpty(response.error))
        {
            print("Error responsing: " + response.error);
        }
        else
        {
            if (response.text.Equals("2"))
            {
                SceneManager.LoadScene("02-Main Menu");
            }
        }
    }

    //登出返回登陆界面
    public void OnLogoff()
    {
        SceneManager.LoadScene("01-Logon");
    }
    //进入其他场景
    public void OnEnterScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
}
