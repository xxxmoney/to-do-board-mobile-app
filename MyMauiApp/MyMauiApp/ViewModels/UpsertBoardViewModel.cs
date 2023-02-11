using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using MyMauiApp.Models;
using MyMauiApp.Services;
using MyMauiApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    [QueryProperty(nameof(Guid), nameof(Guid))]
    public partial class UpsertBoardViewModel : ObservableObject
    {
        private readonly IBoardService boardService;
        
        [ObservableProperty]
        Board board;

        [ObservableProperty]
        string guid;

        public ICommand SaveCommand { get; }

        public UpsertBoardViewModel(IBoardService boardService)
        {
            this.boardService = boardService;

            this.SaveCommand = new AsyncRelayCommand(this.SaveBoardAsync);
        }

        private async Task SaveBoardAsync()
        {
            await this.boardService.UpsertBoardAsync(this.Board);

            await Shell.Current.GoToAsync("..");
        }

        public async Task LoadBoardAsync()
        {
            if (!string.IsNullOrWhiteSpace(this.Guid))
            {
                this.Board = await this.boardService.GetBoardAsync(System.Guid.Parse(this.Guid));
            }
            else
            {
                this.Board = new Board { Guid = System.Guid.NewGuid(), Groups = new List<Group>() };
            }
        }
    }
}
