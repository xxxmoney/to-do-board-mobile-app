using MyMauiApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Services
{
    /// <summary>
    /// Used for getting and updating boards.
    /// </summary>
    public interface IBoardService
    {
        Task<List<Board>> GetBoardsAsync();
        Task<Board> GetBoardAsync(Guid guid);
        Task<Board> UpsertBoardAsync(Board board);
    }

    public class BoardService : IBoardService
    {
        private const string KEY = "boards";

        public Task<List<Board>> GetBoardsAsync()
        {
            string json = Preferences.Get(KEY, null);

            if (json == null)
            {
                return Task.FromResult(new List<Board>());
            }

            return Task.FromResult(JsonConvert.DeserializeObject<List<Board>>(json));
        }

        public async Task<Board> GetBoardAsync(Guid guid)
        {
            return (await this.GetBoardsAsync()).FirstOrDefault(board => board.Guid == guid);
        }

        public async Task<Board> UpsertBoardAsync(Board board)
        {
            var boards = await this.GetBoardsAsync();
            
            // If there is a board with the same guid, update it.
            var existingBoard = boards.FirstOrDefault(b => b.Guid == board.Guid);
            if (existingBoard != null)
            {
                boards.Remove(existingBoard);
            }

            boards.Add(board);

            string json = JsonConvert.SerializeObject(boards);
            Preferences.Set(KEY, json);

            return board;
        }
    }
}
