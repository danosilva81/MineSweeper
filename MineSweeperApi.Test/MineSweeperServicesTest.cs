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
        private Mock<ILogger<MineSweeperService>> LoggerMocked;
        private Mock<IMineSweeperRepository> MineSweeperRepoMocked;

        public MineSweeperServicesTest() 
        {
            MineSweeperRepoMocked = new Mock<IMineSweeperRepository>();
            LoggerMocked= new Mock<ILogger<MineSweeperService>>();            
        }

        private void SetupMockedServiceGame(int xDim, int yDim, List<int> bombsPostion) 
        {
            var game = MineSweeperService.CreateVariableGame(xDim, yDim, bombsPostion);
            MineSweeperRepoMocked.Setup(x => x.GetGameById(It.IsAny<string>())).Returns(game);
            _mineSweeperService = new MineSweeperService(MineSweeperRepoMocked.Object, LoggerMocked.Object);
        }

        [Fact]
        public void RevealCellPosition_RevealBlankPos_ShoulRevealCrrectCells()
        {
            SetupMockedServiceGame(3, 3, new List<int>() { 0 });

            var game = _mineSweeperService.RevealCellPosition("abc1", 8);

            Assert.True(game.MineCellCollection[4].IsRevealed);
            Assert.True(game.MineCellCollection[5].IsRevealed);
            Assert.True(game.MineCellCollection[7].IsRevealed);
            Assert.True(game.MineCellCollection[2].IsRevealed);

            Assert.False(game.MineCellCollection[0].IsRevealed);
        }

        [Fact]
        public void RevealCell_NoSquareGame_RevealBlankPos_ShoulRevealCrrectCells()
        {
            SetupMockedServiceGame(3, 4, new List<int>() { 1, 7 });

            var game = _mineSweeperService.RevealCellPosition("abc1", 11);

            Assert.False(game.MineCellCollection[9].IsRevealed);
            Assert.False(game.MineCellCollection[10].IsRevealed);
            Assert.True(game.MineCellCollection[11].IsRevealed);

            Assert.Equal(1, game.MineCellCollection[0].NumberOfAdjacentBombs);
            Assert.Equal(1, game.MineCellCollection[2].NumberOfAdjacentBombs);

            Assert.Equal(2, game.MineCellCollection[3].NumberOfAdjacentBombs);
            Assert.Equal(2, game.MineCellCollection[4].NumberOfAdjacentBombs);
            Assert.Equal(2, game.MineCellCollection[5].NumberOfAdjacentBombs);

            Assert.Equal(1, game.MineCellCollection[9].NumberOfAdjacentBombs);
            Assert.Equal(1, game.MineCellCollection[10].NumberOfAdjacentBombs);
            Assert.Equal(1, game.MineCellCollection[11].NumberOfAdjacentBombs);

        }
    }
}
