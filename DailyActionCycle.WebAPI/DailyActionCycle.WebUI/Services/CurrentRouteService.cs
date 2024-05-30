namespace DailyActionCycle.WebUI.Services;

public interface ICurrentRouteService {
    
    string CurrentRoute { get; }
    public void SetCurrentRoute(string currentRoute);
}

public class CurrentRouteService : ICurrentRouteService
{
    public string CurrentRoute { get; private set; } = string.Empty;

    public event Action OnChange;

    public void SetCurrentRoute(string route)
    {
        CurrentRoute = route;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}