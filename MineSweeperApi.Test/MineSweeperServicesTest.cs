using Microsoft.Extensions.Logging;
using MineSweeperAPI.Interfaces;
using MineSweeperAPI.Models;
using MineSweeperAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MineSweeperApi.Test
{
    public class MineSweeperServicesTest
    {
        private MineSweeperService _mineSweeperService;

        public MineSweeperServicesTest() 
        {
            Mock<IMineSweeperRepository> MineSweeperRepoMocked = new Mock<IMineSweeperRepository>();
            Mock<ILogger<MineSweeperService>> LoggerMocked= new Mock<ILogger<MineSweeperService>>();

            var game = MineSweeperService.CreateVariableGame(3, 3, new List<int>() { 0 });

            MineSweeperRepoMocked.Setup(x => x.GetGameById(It.IsAny<string>())).Returns(game);
            //MineSweeperRepoMocked.Setup(x => x.UpdateGame(It.IsAny<MineSweeperGame>()));

            _mineSweeperService = new MineSweeperService(MineSweeperRepoMocked.Object, LoggerMocked.Object);
        }

        [Fact]
        public void RevealCellPosition_RevealBlankPos_ShoulRevealCrrectCells()
        {           
            var game = _mineSweeperService.RevealCellPosition("abc1", 8);

            Assert.True(game.MineCellCollection[4].IsRevealed);
            Assert.True(game.MineCellCollection[5].IsRevealed);
            Assert.True(game.MineCellCollection[7].IsRevealed);
            Assert.True(game.MineCellCollection[2].IsRevealed);

            Assert.False(game.MineCellCollection[0].IsRevealed);
        }
    }
}
