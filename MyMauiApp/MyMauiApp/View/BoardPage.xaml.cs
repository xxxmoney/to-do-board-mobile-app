using MyMauiApp.ViewModels;

namespace MyMauiApp.View;

public partial class BoardPage : ContentPage
{
	public BoardPage(BoardViewModel boardViewModel)
	{
		InitializeComponent();
		this.BindingContext = boardViewModel;
	}
}