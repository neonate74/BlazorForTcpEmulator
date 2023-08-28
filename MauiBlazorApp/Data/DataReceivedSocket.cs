using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp.Data
{
    public class DataReceivedSocket
    {
        public DataReceivedSocket(Socket socket, object data) { 
            this.ReceivedData = data;
            this.ClientSocket = socket;
        }

        public object ReceivedData { get; set; }
        public Socket ClientSocket { get; set; }
    }
}
