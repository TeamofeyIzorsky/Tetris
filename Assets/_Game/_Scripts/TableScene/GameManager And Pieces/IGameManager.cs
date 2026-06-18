using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager : IPauseUpdatable
{
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action OnGameOver;
    public event Action<ITetrisField, Piece> OnGameManagerTickOver;

    public Piece GetCurrentPiece();
}
