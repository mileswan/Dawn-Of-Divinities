using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class HttpClient : MonoBehaviour {

    public string IP = "172.16.8.110";
    private const int PORT = 8844;

    public InputField inputUsername;
    public InputField inputPassword;

    private Socket client;
    private string msg,ip;

    // Use this for initialization
    void Start () {
        try
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(IP, PORT);
            Debug.Log("连接服务器成功\r\n");

            Thread threadReceive = new Thread(ReceiveMsg);
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }
        catch
        {
            Debug.Log("连接服务器失败\r\n");
        }
    }

    public void OnLogin()
    {
        if (client == null)
        {
            Start();
        }

        byte[] buffer = Encoding.UTF8.GetBytes("userName:" + inputUsername.text + " password:" + inputPassword.text);
        client.Send(buffer);

    }

    private void ReceiveMsg()
    {
        byte[] buffer = new byte[1024 * 1024];
        int len = 0;
        while (true)
        {
            len = client.Receive(buffer);
            //区分是客户端来了，还是消息来了  
            if (buffer[0] == 1)//客户端  
            {
                ip = Encoding.UTF8.GetString(buffer, 1, len - 1);
            }
            else//文本消息  
            {
                msg = Encoding.UTF8.GetString(buffer, 1, len - 1);
                Debug.Log(msg);
            }
        }
        
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
        client.Shutdown(SocketShutdown.Both);
        client.Close();
        Application.Quit();
    }
}
