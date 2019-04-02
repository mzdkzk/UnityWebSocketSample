using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class Sample : MonoBehaviour
{
    WebSocket ws;
    bool isActive;
    Text label;

    void Start()
    {
        ws = new WebSocket("ws://localhost:3000/socket.io/?EIO=3&transport=websocket");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("WebSocket Message Type: " + e.Type + ", Data: " + e.Data);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        label = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Input.GetKeyUp("s"))
        {
            ws.Send("Test Message");
        }

        // タイムアウトやエラーをキャッチする
        if (isActive != ws.IsAlive)
        {
            label.text = "未接続";
            isActive = false;
        }
    }

    public void WsConnect()
    {
        ws.Connect();
        label.text = "接続済み";
        isActive = true;
    }

    void OnDestroy()
    {
        ws.Close();
        ws = null;
    }
}
