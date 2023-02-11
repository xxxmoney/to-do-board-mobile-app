using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Models;
using MyMauiApp.Services;
using MyMauiApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    [QueryProperty(nameof(Guid), nameof(Guid))]
    public partial class BoardsViewModel : ObservableObject
    {
        private readonly IBoardService boardService;

        public MvvmHelpers.ObservableRangeCollection<Board> Boards { get; set; }

        public ICommand EditCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ExploreCommand { get; }

        [ObservableProperty]
        string guid;

        public BoardsViewModel(IBoardService boardService)
        {
            this.boardService = boardService;

            this.EditCommand = new AsyncRelayCommand<Board>(this.EditBoardAsync);
            this.AddCommand = new AsyncRelayCommand(this.AddBoardAsync);
            this.ExploreCommand = new AsyncRelayCommand<Board>(this.ExploreBoardAsync);
            this.Boards = new MvvmHelpers.ObservableRangeCollection<Board>();
        }

        public async Task LoadBoardsAsync()
        {
            var boards = await this.boardService.GetBoardsAsync();
            this.Boards.Clear();
            this.Boards.AddRange(boards);
        }

        private async Task EditBoardAsync(Board board)
        {
            await Shell.Current.GoToAsync($"{nameof(UpsertBoardPage)}?guid={board.Guid.ToString("N")}");
        }

        private async Task AddBoardAsync()
        {
            await Shell.Current.GoToAsync(nameof(UpsertBoardPage));
        }

        private async Task ExploreBoardAsync(Board board)
        {
            await Shell.Current.GoToAsync($"{nameof(BoardPage)}?guid={board.Guid.ToString("N")}");
        }


    }
}
