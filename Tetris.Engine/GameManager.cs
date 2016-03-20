﻿using System;
using Tetris.Engine.Extensions;
using Tetris.Engine.GameStates;
using Tetris.Engine.GameStates.Interfaces;

namespace Tetris.Engine
{
    public class GameManager
    {
        private readonly BoardManager boardManager;

        public virtual IGameState GameState { get; private set; }

        public GameManager(int height, int width)
        {
            var gameBoard = new bool[height][];
            for (var i = 0; i < height; i++)
            {
                gameBoard[i] = new bool[width];
            }

            this.boardManager = new BoardManager(gameBoard);
            this.GameState = new Playing();
        }

        public BoardManager BoardManager => boardManager;
        public GameStats GameStats => boardManager.GameStats;
        public Block ActiveBlock => boardManager.ActiveBlock;

        public void OnGameLoopStep()
        {
            if (this.GameState.IsPaused())
            {
                return;
            }

            if (this.GameState.IsGameOver())
            {
                return;
            }

            if (boardManager.ActiveBlock == null)
            {
                try
                {
                    boardManager.SpawnBlock();
                }
                catch (Exception)
                {
                    this.GameState = new GameOver();
                }
                return;
            }

            MoveBlock(Move.Down);
        }

        public bool MoveBlock(Move move)
        {
            return boardManager.Move(move);
        }
    }
}