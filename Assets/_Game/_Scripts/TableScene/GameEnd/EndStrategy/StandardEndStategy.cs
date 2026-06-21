using UnityEngine;

public class StandardEndStategy : IEndStrategy
{
    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IPieceController pieceController)
    {
        pieceController.OnSpawnBlocked += gameEndController.GameEnd;

    }
}
