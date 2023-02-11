using MyMauiApp.View;

namespace MyMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Routes
        Routing.RegisterRoute(nameof(UpsertBoardPage), typeof(UpsertBoardPage));
        Routing.RegisterRoute(nameof(BoardPage), typeof(BoardPage));
        //Routing.RegisterRoute(nameof(BoardsPage), typeof(BoardsPage));
        //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
