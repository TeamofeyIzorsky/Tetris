using System;
using UnityEngine;

public interface IPieceController : IPauseUpdatable
{
    public Piece GetCurrentPiece();

    public event Action<Piece, bool> OnUpdateHold;
    public event Action OnCreateNewPiece;
    public event Action OnSpawnBlocked;
    public event Action<Piece> PieceControllerTickOver;
}
