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

            this.MoveItemCommand = new RelayCommand<(Item, Group, Group)>(this.MoveItem);
            this.SaveCommand = new AsyncRelayCommand(this.SaveAsync);
            this.AddItemCommand = new RelayCommand<Group>(AddItem);
            this.EditCommand = new AsyncRelayCommand(this.EditAsync);
            this.AddGroupCommand = new RelayCommand(this.AddGroup);
        }
        
        public ICommand MoveItemCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand AddGroupCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        
        private void MoveItem((Item, Group, Group) args)
        {
            var item = args.Item1;
            var sourceGroup = args.Item2;
            var targetGroup = args.Item3;

            if (sourceGroup == null || item == null || targetGroup == null)
                return;

            sourceGroup.Items.Remove(item);
            targetGroup.Items.Add(item);
        }
        
        private void AddItem(Group group)
        {
            var item = new Item { Text = "New Item" };
            group.Items.Add(item);
        }

        private void AddGroup()
        {
            var group = new Group { Name = "New Group", Items = new MvvmHelpers.ObservableRangeCollection<Item>() };
            this.Board.Groups.Add(group);
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
