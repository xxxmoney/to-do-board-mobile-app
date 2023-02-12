using MyMauiApp.ViewModels;

namespace MyMauiApp.View;

public partial class UpsertBoardPage : ContentPage
{
	public UpsertBoardPage(UpsertBoardViewModel upsertBoardViewModel)
	{
		InitializeComponent();
        this.BindingContext = upsertBoardViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((UpsertBoardViewModel)this.BindingContext).LoadBoardAsync();
    }
}