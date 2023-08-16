using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MauiAppForMainConsole.Services;

public class ServerListener : INotification
{
    private Socket m_ServerSocket;
    private List<Socket> m_ClientSocket;

    private int m_Port = 8001;
    private int bufferSize = 4096;

    private byte[] szData;

    public event EventHandler AcceptionCompleted;

    private EventHandler eventHandler;

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
                    byte[] szData = e.Buffer;
                    string sData = Encoding.UTF8.GetString(szData);

                    string Test = sData.Replace("\0", "");

                    Thread.Sleep(1000);
                    for (int i = 0; i < szData.Length; i++)
                        szData[i] = 0;

                    e.SetBuffer(szData, 0, bufferSize);

                    clientSocket.ReceiveAsync(e);
                }
            }
            else
            {
                clientSocket.Disconnect(false);
                m_ClientSocket.Remove(clientSocket);

                Console.WriteLine($"'{clientSocket.LocalEndPoint.ToString()}'의 연결이 끊어졌습니다.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}

