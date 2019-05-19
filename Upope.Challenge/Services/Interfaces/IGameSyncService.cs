﻿using System.Threading.Tasks;
using Upope.Challenge.Services.Models;

namespace Upope.Game.Services.Interfaces
{
    public interface IGameSyncService
    {
        Task SyncGameTable(CreateOrUpdateGameModel model, string accessToken);
    }
}
