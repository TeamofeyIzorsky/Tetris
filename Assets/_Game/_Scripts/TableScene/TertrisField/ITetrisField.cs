using System;
using System.Collections.Generic;
using UnityEngine;

public interface ITetrisField
{
    public event Action<IReadOnlyList<Vector2Int>, Cell[,], Piece, IReadOnlyList<Vector2Int>> OnAfterUpdate;
    public event Action<IReadOnlyList<Piece>> OnUpdateBag;
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action<int, int> OnDeleteLinesEnd;
    public event Action OnGameOver;
}
