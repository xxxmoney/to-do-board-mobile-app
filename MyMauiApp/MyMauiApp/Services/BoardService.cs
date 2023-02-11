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
        /// <summary>
        /// Gets all boards.
        /// </summary>
        /// <returns></returns>
        Task<List<Board>> GetBoardsAsync();
        /// <summary>
        /// Gets a board by its Id.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<Board> GetBoardAsync(Guid guid);
        /// <summary>
        /// Updates or inserts a board.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        Task<Board> UpsertBoardAsync(Board board);
        /// <summary>
        /// Deletes a board.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task DeleteBoardAsync(Guid guid);
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

        public async Task DeleteBoardAsync(Guid guid)
        {
            var boards = await this.GetBoardsAsync();

            // If there is a board with the same guid, update it.
            var existingBoard = boards.FirstOrDefault(b => b.Guid == guid);
            if (existingBoard != null)
            {
                boards.Remove(existingBoard);
            }

            string json = JsonConvert.SerializeObject(boards);
            Preferences.Set(KEY, json);
        }
    }
}
