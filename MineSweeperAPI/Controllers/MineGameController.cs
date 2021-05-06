using Microsoft.AspNetCore.Mvc;
using MineSweeperAPI.Models;
using MineSweeperAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MineGameController : ControllerBase
    {
        private readonly MineSweeperService _MineSweeperService;

        public MineGameController(MineSweeperService MineSweeperService)
        {
            _MineSweeperService = MineSweeperService;
        }

        [HttpPost]
        public ActionResult<MineSweeperGame> Create(int xDim, int yDim)
        {
            var game = _MineSweeperService.CreateNewGame(xDim, yDim);
            return CreatedAtRoute("GetGame", new { id = game.Id.ToString() }, game);
        }

        [HttpGet]
        public ActionResult<List<MineSweeperGame>> Get() =>
            _MineSweeperService.GetListOfGames();


        [HttpGet("GetGame/{id:length(24)}", Name = "GetGame")]
        public ActionResult<MineSweeperGame> Get(string id)
        {
            var game = _MineSweeperService.GetGameById(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("GetGameByUser/{userId:length(24)}", Name = "GetGameByUser")]
        public ActionResult<MineSweeperGame> GetGameByUser(string userId)
        {
            return new MineSweeperGame() { Id = userId };
        }

        [HttpGet("RevealCell/{id:length(24)}")]
        public ActionResult<MineSweeperGame> RevealCell(string gameId, int position)
        {
            var game = _MineSweeperService.RevealCellPosition(gameId, position);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("MarkCellAsBomb/{id:length(24)}")]
        public ActionResult<MineSweeperGame> MarkCellAsBomb(string gameId, int position)
        {
            var game = _MineSweeperService.MarkCellAsBomb(gameId, position);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }
    }
}
