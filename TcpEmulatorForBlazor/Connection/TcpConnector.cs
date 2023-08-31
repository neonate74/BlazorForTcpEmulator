using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpEmulator.Connection
{
    public class TcpConnector
    {
        private Socket clientSocket;
        private Socket serverSocket;

        private byte[] recvBuffer;

        private int MaxSize = 4096;

        //private string HOST = "192.168.0.101";
        private string HOST = "172.30.152.119";

        private int PORT = 8001;

        public event EventHandler? DataReceived;
        public event EventHandler? ConnectedConnection;
        public event EventHandler? DisConnectedConnection;

        public string ReceivedDataString { get; set; } = "";
        public bool IsConnected
        {
            get
            {
                return (clientSocket == null) ? false : clientSocket.Connected;
            }
        }

        public void DoConnect()
        {
            recvBuffer = new byte[MaxSize];
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.BeginConnect();
        }

        public void DoDisconnect()
        {
            this.BeginSend("RequestDisconnect");

            //clientSocket.Close();
            //clientSocket.Dispose();

            //if (DisConnectedConnection != null)
            //{
            //    DisConnectedConnection.Invoke(clientSocket, new EventArgs());
            //}
        }

        public void BeginConnect()
        {
            try
            {
                clientSocket.BeginConnect(HOST, PORT, new AsyncCallback(ConnectCallback), clientSocket);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[{ex.NativeErrorCode.ToString()}]서버 접속에 실패하였습니다: {ex.Message}");
                Thread.Sleep(3000);
                DoConnect();
            }
        }

        private void ConnectCallback(IAsyncResult IAR)
        {
            try
            {
                if (IAR.AsyncState != null)
                {
                    Socket tempSocket = (Socket)IAR.AsyncState;
                    if (tempSocket.RemoteEndPoint != null)
                    {
                        IPEndPoint svrEP = (IPEndPoint)tempSocket.RemoteEndPoint;

                        Console.WriteLine($"서버 접속 성공: {svrEP.Address}");

                        if (ConnectedConnection != null)
                        {
                            ConnectedConnection.Invoke(tempSocket, new EventArgs());
                        }

                        tempSocket.EndConnect(IAR);
                        serverSocket = tempSocket;

                        Receive();
                    }
                    else
                    {
                        throw new SocketException((int)SocketError.NotConnected);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.NotConnected)
                {
                    Console.WriteLine($"서버 접속 실패 Callback: {ex.Message}");
                    Thread.Sleep(3000);
                    this.BeginConnect();
                }
            }
        }

        public void Receive()
        {
            serverSocket.BeginReceive(this.recvBuffer, 0, recvBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), serverSocket);
        }

        private void OnReceiveCallback(IAsyncResult IAR)
        {
            try
            {
                if (IAR.AsyncState != null)
                {
                    Socket tempSocket = (Socket)IAR.AsyncState;
                    int nReadSize = tempSocket.EndReceive(IAR);

                    if (nReadSize > 0)
                    {
                        this.ReceivedDataString = Encoding.UTF8.GetString(recvBuffer, 0, nReadSize);

                        if (this.DataReceived != null)
                        {
                            DataReceived.Invoke(tempSocket, new EventArgs());
                        }

                        if (this.ReceivedDataString != "DisconnectOK")
                        {
                            this.Receive();
                        }   
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                {
                    this.BeginConnect();
                }
            }
        }

        public void BeginSend(string message)
        {
            try
            {
                if (clientSocket.Connected)
                {
                    byte[] buffer = new UTF8Encoding().GetBytes(message);
                    clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), message);
                }
                else
                {
                    Console.WriteLine($"ClientSocket(To:{HOST}) is not connected.");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"전송 에러: {ex.Message}");
            }
        }

        private void SendCallback(IAsyncResult IAR)
        {
            if (IAR.AsyncState != null)
            {
                string message = (string)IAR.AsyncState;
                Console.WriteLine($"전송 완료 Callback: {message}");
            }
        }
    }
}
