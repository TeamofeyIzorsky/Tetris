using System;

public interface IGameManager : IPauseUpdatable
{
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action OnSpawnBlocked;
    public event Action<ITetrisField, Piece> OnGameManagerTickOver;

    public Piece GetCurrentPiece();
}
