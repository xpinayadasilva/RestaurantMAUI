using Microsoft.Maui.Controls;
using Restaurant.Pages;

namespace Restaurant;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(PlatosPage), typeof(PlatosPage));
        Routing.RegisterRoute(nameof(ListaPlatosPage), typeof(ListaPlatosPage));
    }
}
