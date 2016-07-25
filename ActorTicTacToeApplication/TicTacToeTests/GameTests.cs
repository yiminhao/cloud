﻿using System;
using Game.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToeTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void AplayerCanOnlyJoinOnce()
        {
            var gameId = ActorId.CreateRandom();
            var game = ActorProxy.Create<IGame>(gameId, "fabric:/ActorTicTacToeApplication");
            var result1 = game.JoinGameAsync(1L, "Player 1").Result;
            var result2 = game.JoinGameAsync(1L, "Player 1").Result;
            Assert.AreEqual(false, result1&result2);
        }

        [TestMethod]
        public void GameCanBeStartedWith2Players()
        {
            var gameId = ActorId.CreateRandom();
            var game = ActorProxy.Create<IGame>(gameId, "fabric:/ActorTicTacToeApplication");
            game.JoinGameAsync(1L, "Player 1").Wait();
            var result = game.MakeMoveAsync(1L, 0, 0).Result;
            Assert.AreEqual(false, result);
        }
    }
}
