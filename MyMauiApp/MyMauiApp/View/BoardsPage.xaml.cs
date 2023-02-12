using MyMauiApp.Models;
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

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var board = (Board)e.SelectedItem;

        if (board == null)
            return;

        await ((BoardsViewModel)this.BindingContext).ExploreBoardAsync(board);
    }
}