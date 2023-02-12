using MyMauiApp.View;

namespace MyMauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Routes
        Routing.RegisterRoute("upsert", typeof(UpsertBoardPage));
        Routing.RegisterRoute("board", typeof(BoardPage));
        Routing.RegisterRoute("main", typeof(MainPage));
    }
}
