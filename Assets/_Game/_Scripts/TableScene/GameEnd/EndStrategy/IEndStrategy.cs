using UnityEngine;

public interface IEndStrategy
{
    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IPieceController pieceController);
}
