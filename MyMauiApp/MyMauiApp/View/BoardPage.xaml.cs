using MyMauiApp.ViewModels;

namespace MyMauiApp.View;

public partial class BoardPage : ContentPage
{
	public BoardPage(BoardViewModel boardViewModel)
	{
		InitializeComponent();
		this.BindingContext = boardViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((BoardViewModel)this.BindingContext).LoadAsync();
    }
}