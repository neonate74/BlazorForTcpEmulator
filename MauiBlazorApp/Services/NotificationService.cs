using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorApp.Services
{
    public class NotificationService
    {
        public static INotification ServerListenerModule { get; set; } = new ServerListener();
    }
}
