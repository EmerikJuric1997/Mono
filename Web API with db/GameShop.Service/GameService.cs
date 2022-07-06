using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameModel;
using GameService.Common;
using GameRepository.Common;

namespace GameShop.Service
{
    public class GameService : IGameService
    {
        private IGameRepository _objGameRepo;

        public GameService(IGameRepository objGameRepo)
        {
            _objGameRepo = objGameRepo;
        }
        public Game AddNewGame(Game value)
        {
            return _objGameRepo.AddNewGame(value);
        }

        public Game Delete(Guid id)
        {
            return _objGameRepo.Delete(id);
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _objGameRepo.GetAllGamesAsync();
        }

        public async Task<Game> GetOneGameAsync(Guid id)
        {
            return await _objGameRepo.GetOneGameAsync(id);
        }

        public Game Put(Guid id, Game value)
        {
            return _objGameRepo.Put(id, value);
        }
    }
}
