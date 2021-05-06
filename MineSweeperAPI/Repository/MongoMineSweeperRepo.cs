using Microsoft.Extensions.Configuration;
using MineSweeperAPI.Interfaces;
using MineSweeperAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperAPI.Repository
{
    public class MongoMineSweeperRepo : IMineSweeperRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<MineSweeperGame> _mineSweeperGames;

        public MongoMineSweeperRepo(
            MongoClient mongoClient,
            IConfiguration configuration)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(configuration.GetSection("MineDatabaseSettings")["DatabaseName"]);
            _mineSweeperGames = _database.GetCollection<MineSweeperGame>("MineSweeperGames");
        }


        public MineSweeperGame CreateMineSweeper(MineSweeperGame game)
        {
            _mineSweeperGames.InsertOne(game);
            return game;
        }

        public MineSweeperGame GetGameById(string id) => _mineSweeperGames.Find<MineSweeperGame>(game => game.Id == id).FirstOrDefault();

        public List<MineSweeperGame> GetListOfGames()
        {
            return _mineSweeperGames.Find(game => true).ToList();
        }

        public void UpdateGame(MineSweeperGame game)
        {
            _mineSweeperGames.ReplaceOne(g => g.Id == game.Id, game);
        }
    }
}
