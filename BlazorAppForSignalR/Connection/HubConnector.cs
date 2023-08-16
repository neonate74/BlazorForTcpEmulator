using Microsoft.AspNetCore.SignalR.Client;
using BlazorAppForSignalR.Data.Models;

namespace BlazorAppForSignalR.Connection;

public sealed class Consumer : IAsyncDisposable
{
    private readonly string HostDomain = Environment.GetEnvironmentVariable("HOST_DOMAIN");

    private HubConnection _hubConnection;

    public Consumer()
    {
        if (HostDomain == null || HostDomain.Length == 0)
        {
            HostDomain = "127.0.0.1";
        }

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{HostDomain}/hub/notifications"))
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<Notification>(
            "NotificationReceived", OnNotificationReceivedAsync);
    }

    //private async Task OnNotificationReceivedAsync(Notification notification)
    //{
    //}

    private void OnNotificationReceivedAsync(Notification notification)
    {
        // Do something meaningful with the notification.
    }

    public Task StartNotificationConnectionAsync() =>
        _hubConnection.StartAsync();

    public Task SendNotificationAsync(string text) => _hubConnection.InvokeAsync("NotifyAll", new Notification(text, DateTime.UtcNow));

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }
    }
}