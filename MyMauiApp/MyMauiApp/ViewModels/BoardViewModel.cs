using CommunityToolkit.Mvvm.ComponentModel;
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
        
        public BoardViewModel(IBoardService boardService)
        {
            this.boardService = boardService;
        }
    }
}
