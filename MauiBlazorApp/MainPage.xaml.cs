using MauiBlazorApp.Services;

namespace MauiBlazorApp
{
    public partial class MainPage : ContentPage
    {
        private int currentCount = 0;

        public int CurrentConnection => currentCount;

        public MainPage()
        {
            InitializeComponent();
            NotificationService.ServerListenerModule.Subscribe(ServerCounter_AcceptionCompleted);

            NotificationService.ServerListenerModule.ConnectionDisconnected += ServerListenerModule_ConnectionDisconnected;
        }

        private void ServerListenerModule_ConnectionDisconnected(object sender, EventArgs e)
        {
            currentCount--;
        }

        private void ServerCounter_AcceptionCompleted(object sender, EventArgs e)
        {
            currentCount++;
        }
    }
}