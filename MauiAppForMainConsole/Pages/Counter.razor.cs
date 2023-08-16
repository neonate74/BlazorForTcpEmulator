using MauiAppForMainConsole.Services;

namespace MauiAppForMainConsole.Pages;

public partial class Counter
{

    private int currentCount = 0;

    public Counter()
    {
    }

    private void IncrementCount()
    {
        currentCount++;
    }

    protected override async Task OnInitializedAsync()
    {
        NotificationService.ServerListenerModule.Subscribe(ServerCounter_AcceptionCompleted);
    }

    private void ServerCounter_AcceptionCompleted(object sender, EventArgs e)
    {
        currentCount++;

        InvokeAsync(StateHasChanged);
    }
}
