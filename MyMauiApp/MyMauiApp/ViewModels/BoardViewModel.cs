using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Models;
using MyMauiApp.Services;
using MyMauiApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    [QueryProperty(nameof(Guid), nameof(Guid))]
    public partial class BoardViewModel : ObservableObject
    {
        private readonly IBoardService boardService;

        [ObservableProperty]
        string guid;

        [ObservableProperty]
        Board board;

        public BoardViewModel(IBoardService boardService)
        {
            this.boardService = boardService;

            this.SaveCommand = new AsyncRelayCommand(this.SaveAsync);
            this.EditCommand = new AsyncRelayCommand(this.EditAsync);
            this.AddGroupCommand = new RelayCommand(this.AddGroup);
        }
        
        public ICommand AddGroupCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        
        public void MoveItem(Item item, Group oldGroup, Group newGroup)
        {
            oldGroup.Items.Remove(item);
            newGroup.Items.Add(item);
        }
        
        public void AddItem(Group group)
        {
            var item = new Item { Text = "New Item" };
            group.Items.Add(item);
        }

        private void AddGroup()
        {
            var group = new Group { Name = "New Group", Items = new MvvmHelpers.ObservableRangeCollection<Item>() };
            this.Board.Groups.Add(group);
        }

        public void RemoveGroup(Group group)
        {
            // Removes the group from the board.
            this.Board.Groups.Remove(group);                        
        }

        public async Task LoadAsync()
        {
            this.Board = await this.boardService.GetBoardAsync(System.Guid.Parse(this.Guid));
        }

        private async Task EditAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(UpsertBoardPage)}?Guid={this.Board.Guid}");
        }

        private Task SaveAsync()
        {
            return boardService.UpsertBoardAsync(this.Board);
        }
    }
}
