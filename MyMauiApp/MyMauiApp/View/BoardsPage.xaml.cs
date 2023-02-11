using MyMauiApp.ViewModels;

namespace MyMauiApp.View;

public partial class BoardsPage : ContentPage
{
	public BoardsPage(BoardsViewModel boardsViewModel)
	{
		InitializeComponent();
        this.BindingContext = boardsViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((BoardsViewModel)this.BindingContext).LoadBoardsAsync();
    }
}