﻿using MineSweeperAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperAPI.Interfaces
{
    public interface IMineSweeperRepository
    {
        public MineSweeperGame CreateMineSweeper(MineSweeperGame game);

        public List<MineSweeperGame> GetListOfGames();

        public MineSweeperGame GetGameById(string id);

        public void UpdateGame(MineSweeperGame game);

        public void DeleteGame(string id);
    }
}
