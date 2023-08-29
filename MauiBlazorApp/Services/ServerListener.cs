using MauiBlazorApp.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MauiBlazorApp.Services;

public class ServerListener : INotification
{
    private Socket m_ServerSocket;
    private List<Socket> m_ClientSocket;

    private int m_Port = 8001;
    private int bufferSize = 4096;

    private byte[] szData;

    private event EventHandler AcceptionCompleted;
    private EventHandler eventHandler;

    public event EventHandler ConnectionDisconnected;
    public event EventHandler<SocketAsyncEventArgs> DataReceived;

    public ServerListener()
    {
        this.DataReceived += ServerListener_DataReceived;
    }

    private void ServerListener_DataReceived(object sender, SocketAsyncEventArgs e)
    {
        if (sender != null)
        {
            Socket clientSocket = (Socket)sender;

            byte[] szData = e.Buffer;
            string sData = Encoding.UTF8.GetString(szData).Replace("\0", "");

            switch (sData)
            {
                case "RequestDisconnect":
                    this.SendData(clientSocket, "DisconnectOK");
                    this.DisConnectTcpClientSocket(clientSocket);
                    break;
            }
        }
    }

    public List<Socket> ClientSocketList
    {
        get { return m_ClientSocket; }
    }

    public void Publish()
    {
        this.BeginListening();
    }

    public void Subscribe(EventHandler ServerListenerModule_AcceptionCompleted)
    {
        this.eventHandler = ServerListenerModule_AcceptionCompleted;

        this.Unsubscribe(this.eventHandler);

        this.AcceptionCompleted += this.eventHandler;
    }

    public void Unsubscribe(EventHandler ServerListenerModule_AcceptionCompleted)
    {
        try
        {
            this.AcceptionCompleted -= this.eventHandler;
        }
        catch { }
    }

    public void BeginListening()
    {
        try
        {
            m_ClientSocket = new List<Socket>();
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, m_Port);

            m_ServerSocket.Bind(ipep);
            m_ServerSocket.Listen(100);

            SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
            socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEventArgs_AcceptCompleted);

            m_ServerSocket.AcceptAsync(socketAsyncEventArgs);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void SocketAsyncEventArgs_AcceptCompleted(object sender, SocketAsyncEventArgs e)
    {
        try
        {
            if (m_ClientSocket != null && e.AcceptSocket != null)
            {
                Socket clientSocket = e.AcceptSocket;
                m_ClientSocket.Add(clientSocket);

                if (AcceptionCompleted != null)
                    AcceptionCompleted.Invoke(this, new EventArgs());

                SocketAsyncEventArgs socketReceiveEventArgs = new SocketAsyncEventArgs();
                szData = new byte[bufferSize];
                socketReceiveEventArgs.SetBuffer(szData, 0, bufferSize);
                socketReceiveEventArgs.UserToken = clientSocket;

                socketReceiveEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEventArgs_ReceiveCompleted);
                clientSocket.ReceiveAsync(socketReceiveEventArgs);

                e.AcceptSocket = null;
            }

            m_ServerSocket.AcceptAsync(e);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void SocketAsyncEventArgs_ReceiveCompleted(object sender, SocketAsyncEventArgs e)
    {
        try
        {
            Socket clientSocket = (Socket)sender;
            if (clientSocket.Connected)
            {
                if (e.BytesTransferred > 0)
                {
                    if (DataReceived != null)
                    {
                        DataReceived.Invoke(clientSocket, e);
                    }

                    Thread.Sleep(500);
                    byte[] szData = e.Buffer;
                    for (int i = 0; i < szData.Length; i++)
                        szData[i] = 0;

                    e.SetBuffer(szData, 0, bufferSize);

                    clientSocket.ReceiveAsync(e);
                }
            }
            else
            {
                this.DisConnectTcpClientSocket(clientSocket);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void DisConnectTcpClientSocket(Socket clientSocket)
    {
        string temp = clientSocket.LocalEndPoint.ToString();
        clientSocket.Disconnect(false);
        m_ClientSocket.Remove(clientSocket);

        Console.WriteLine($"'{temp}'의 연결이 끊어졌습니다.");
    }

    public void SendData(Socket client = null, string message = "")
    {
        byte[] data = Encoding.UTF8.GetBytes(message.Trim().Length == 0 ? $"{client.RemoteEndPoint.ToString()} : connected" : message);

        Task<int> task = client.SendAsync(data, SocketFlags.None);
    }

    public void SendData(string message = "")
    {
        int lastIndex = m_ClientSocket.Count - 1;

        byte[] data = Encoding.UTF8.GetBytes(message.Trim().Length == 0 ? $"{m_ClientSocket[lastIndex].RemoteEndPoint.ToString()} : connected" : message);

        m_ClientSocket[lastIndex].SendAsync(data, SocketFlags.None);
    }

}