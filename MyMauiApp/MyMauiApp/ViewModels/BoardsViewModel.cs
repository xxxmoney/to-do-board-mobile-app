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
    public partial class BoardsViewModel : ObservableObject
    {
        private readonly IBoardService boardService;

        public MvvmHelpers.ObservableRangeCollection<Board> Boards { get; set; }

        public ICommand EditCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ExploreCommand { get; }

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

        public async Task EditBoardAsync(Board board)
        {
            await Shell.Current.GoToAsync($"{nameof(UpsertBoardPage)}?Guid={board.Guid.ToString("N")}");
        }

        public async Task AddBoardAsync()
        {
            await Shell.Current.GoToAsync(nameof(UpsertBoardPage));
        }

        public async Task ExploreBoardAsync(Board board)
        {
            await Shell.Current.GoToAsync($"{nameof(BoardPage)}?Guid={board.Guid.ToString("N")}");
        }


    }
}
