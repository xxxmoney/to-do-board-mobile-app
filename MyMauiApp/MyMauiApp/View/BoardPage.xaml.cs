using MyMauiApp.Models;
using MyMauiApp.ViewModels;

namespace MyMauiApp.View;

public partial class BoardPage : ContentPage
{
    private readonly BoardViewModel boardViewModel;
    private Item draggedItem;

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
        var group = FindGroup(item);
        group.Items.Remove(item);
    }

    private Group FindGroup(Item item)
    {
        foreach (var group in this.boardViewModel.Board.Groups)
        {
            if (group.Items.Contains(item))
            {
                return group;
            }
        }

        return null;
    }

    private void ItemMove_Dropped(object sender, DropEventArgs e)
    {
        // Gets the old group of dragged item.
        var oldGroup = FindGroup(this.draggedItem);

        // Gets the new group of dragged item - from Frame binding context.
        var source = (DropGestureRecognizer)sender;
        var group = (Group)source.BindingContext;

        // Removes the item from the old group.
        oldGroup.Items.Remove(this.draggedItem);

        // Adds the item to the new group.
        group.Items.Add(this.draggedItem);

        // Resets the dragged item.
        this.draggedItem = null;
    }

    private void ItemMove_DragStarting(object sender, DragStartingEventArgs e)
    {
        // Gets the dragged item from the button's command parameter.
        var source = (DragGestureRecognizer)sender;
        this.draggedItem = (Item)source.BindingContext;
    }
}