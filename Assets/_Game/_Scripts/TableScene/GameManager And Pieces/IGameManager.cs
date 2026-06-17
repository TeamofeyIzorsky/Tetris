using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    public void Init(IBag bag, ITetrisField tetrisField, IPlayerInput playerInput);

    public event Action<IReadOnlyList<Piece>> OnUpdateBag;
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action OnGameOver;
    public event Action<ITetrisField, Piece> OnTickOver;
}
