using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameModel;

namespace GameService.Common
{
    public interface IGameService
    {
        Game AddNewGame(Game value);
        Game Delete(Guid id);
        Task<List<Game>> GetAllGamesAsync();
        Task<Game> GetOneGameAsync(Guid id);
        Game Put(Guid id, Game value);

    }
}
