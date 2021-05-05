using MineSweeperAPI.Interfaces;
using MineSweeperAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperAPI.Services
{
    public class MineSweeperService
    {
        private IMineSweeperRepository _mineRepository;

        public MineSweeperService(IMineSweeperRepository mineRepository)
        {
            _mineRepository = mineRepository;
        }

        public MineSweeperGame CreateNewGame(int xDim, int yDim, int numberOfBombsParam = 0)
        {
            int numOfBombs = numberOfBombsParam != 0 ? numberOfBombsParam : (int)Math.Round((double)(xDim * yDim / 5));
            List<int> bombsPositions = GenerateRandomBombPositions(xDim * yDim, numOfBombs);
            return CreateNewGame(xDim, yDim, bombsPositions);
        }

        public MineSweeperGame CreateNewGame(int xDim, int yDim, List<int> listOfBombsPositions)
        {
            var game = new MineSweeperGame()
            {
                XDimension = xDim,
                YDimension = yDim,
                NumberOfBombs = listOfBombsPositions.Count,
                MineCellCollection = new MineCell[xDim * yDim]
            };

            var mineCellsQty = game.MineCellCollection.Length;

            for (int i = 0; i < mineCellsQty; i++)
            {
                game.MineCellCollection[i] = new MineCell()
                {
                    ArrayPostion = i,
                    XPos = i % xDim,
                    YPos = i / xDim,
                    IsBomb = (listOfBombsPositions.Contains(i))
                };

                if (!game.MineCellCollection[i].IsBomb)
                    SetAmmountOfAdjacentBombs(game.MineCellCollection[i], listOfBombsPositions, xDim, yDim);
            }

            game.GameIsOver = false;

            _mineRepository.CreateMineSweeper(game);

            return game;
        }

        public List<int> GenerateRandomBombPositions(int cellsQty, int numberOfBombs)
        {
            if (numberOfBombs > cellsQty)
                throw new ArgumentException("Is not posible has more bombs than spaces.");

            var listOfBombsPositions = new List<int>();
            var randomGen = new Random();
            while (listOfBombsPositions.Count < numberOfBombs)
            {
                var newRandomNumber = randomGen.Next(0, cellsQty - 1);
                if (!listOfBombsPositions.Contains(newRandomNumber))
                    listOfBombsPositions.Add(newRandomNumber);
            }

            return listOfBombsPositions;
        }

        private void SetAmmountOfAdjacentBombs(MineCell mineSpace, List<int> bombsPosition, int XDimension, int YDimension)
        {
            foreach (var bomb in bombsPosition)
            {
                var xBombPos = bomb % XDimension;
                var yBombPos = bomb / YDimension;
                if ((xBombPos == mineSpace.XPos || xBombPos == mineSpace.XPos - 1 || xBombPos == mineSpace.XPos + 1) &&
                    (yBombPos == mineSpace.YPos || yBombPos == mineSpace.YPos - 1 || yBombPos == mineSpace.YPos + 1))
                    mineSpace.NumberOfAdjacentBombs++;
            }
        }

        public List<MineSweeperGame> GetListOfGames() => _mineRepository.GetListOfGames();

        public MineSweeperGame GetGameById(string id) => _mineRepository.GetGameById(id);

        public MineSweeperGame RevealCellPosition(string gameId, int position)
        {
            var game = _mineRepository.GetGameById(gameId);

            if (game != null)
            {

            }

            return game;
        }

        public void RevealCell(MineSweeperGame game, int position)
        {
            if (game.MineCellCollection[position].IsBomb)
                GameOver(game);
            else
            {
                game.MineCellCollection[position].IsRevealed = true;
                if (game.MineCellCollection[position].NumberOfAdjacentBombs == 0)
                {
                    int pos1 = (position - game.XDimension) - 1;
                    if (IsPositionInBound(game, pos1))
                        RevealCell(game, pos1);

                    int pos2 = (position - game.XDimension);
                    if (IsPositionInBound(game, pos2))
                        RevealCell(game, pos2);

                    int pos3 = (position - game.XDimension) + 1;
                    if (IsPositionInBound(game, pos3))
                        RevealCell(game, pos3);

                    int pos4 = position + 1;
                    if (IsPositionInBound(game, pos4))
                        RevealCell(game, pos4);

                    int pos5 = position + 1;
                    if (IsPositionInBound(game, pos5))
                        RevealCell(game, pos5);

                    int pos6 = (position + game.XDimension) - 1;
                    if (IsPositionInBound(game, pos6))
                        RevealCell(game, pos6);

                    int pos7 = (position + game.XDimension);
                    if (IsPositionInBound(game, pos7))
                        RevealCell(game, pos7);

                    int pos8 = (position + game.XDimension) + 1;
                    if (IsPositionInBound(game, pos8))
                        RevealCell(game, pos8);
                }
            }
        }

        private void GameOver(MineSweeperGame game)
        {
            foreach (var item in game.MineCellCollection)
                item.IsRevealed = true;

            game.GameIsOver = true;
        }

        private bool IsPositionInBound(MineSweeperGame game, int position)
        {
            return position >= 0 && position < game.XDimension * game.YDimension;
        }
    }
}
