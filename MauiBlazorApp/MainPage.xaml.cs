using MauiBlazorApp.Services;
using Microsoft.Extensions.Configuration;

namespace MauiBlazorApp
{
    public partial class MainPage : ContentPage
    {
        private int currentCount = 0;

        public int CurrentConnection => currentCount;
        public event EventHandler ConnectionChanged;

        private readonly IConfiguration _configuration;

        public IConfiguration Configuration
        {
            get { return _configuration; }
        }

        public MainPage(IConfiguration config)
        {
            InitializeComponent();
            NotificationService.ServerListenerModule.Subscribe(ServerCounter_AcceptionCompleted);

            NotificationService.ServerListenerModule.ConnectionDisconnected += ServerListenerModule_ConnectionDisconnected;

            _configuration = config;
        }

        private void ServerListenerModule_ConnectionDisconnected(object sender, EventArgs e)
        {
            currentCount--;

            if (ConnectionChanged != null)
                ConnectionChanged.Invoke(this, EventArgs.Empty);
        }

        private void ServerCounter_AcceptionCompleted(object sender, EventArgs e)
        {
            currentCount++;

            if (ConnectionChanged != null)
                ConnectionChanged.Invoke(this, EventArgs.Empty);

            NotificationService.ServerListenerModule.SendData();
        }
    }
}