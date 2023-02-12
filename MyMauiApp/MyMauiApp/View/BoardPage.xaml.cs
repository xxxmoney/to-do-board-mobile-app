using MyMauiApp.Models;
using MyMauiApp.ViewModels;

namespace MyMauiApp.View;

public partial class BoardPage : ContentPage
{
    private readonly BoardViewModel boardViewModel;

    public BoardPage(BoardViewModel boardViewModel)
	{
		InitializeComponent();
        this.boardViewModel = boardViewModel;
        this.BindingContext = this.boardViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await this.boardViewModel.LoadAsync();
    }

    private void ButtonAddGroup_Clicked(object sender, EventArgs e)
    {
        // Gets group from the button's command parameter.
        var button = (Button)sender;
        var group = (Group)button.BindingContext;

        // Adds a new item to the group.
        this.boardViewModel.AddItem(group);
    }

    private void ButtonRemoveGroup_Clicked(object sender, EventArgs e)
    {
        // Gets group from the button's command parameter.
        var button = (Button)sender;
        var group = (Group)button.BindingContext;

        // Removes the group from the board.
        this.boardViewModel.RemoveGroup(group);
    }

    private void RemoveItem_Clicked(object sender, EventArgs e)
    {
        // Gets item from the button's command parameter.
        var button = (Button)sender;
        var item = (Item)button.BindingContext;

        // Goes through all groups and finds the group that contains the item.
        foreach (var group in this.boardViewModel.Board.Groups)
        {
            if (group.Items.Contains(item))
            {
                // Removes the item from the group.
                group.Items.Remove(item);
                break;
            }
        }
    }
}