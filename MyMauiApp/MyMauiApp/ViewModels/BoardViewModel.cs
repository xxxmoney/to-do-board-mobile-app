using CommunityToolkit.Mvvm.ComponentModel;
using MyMauiApp.Models;
using MyMauiApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public async Task LoadAsync()
        {
            this.Board = await this.boardService.GetBoardAsync(System.Guid.Parse(this.Guid));
        }
    }
}
