using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager : IPauseUpdatable
{
    public event Action<IReadOnlyList<Piece>> OnUpdateBag;
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action OnGameOver;
    public event Action<ITetrisField, Piece> OnGameManagerTickOver;
}
