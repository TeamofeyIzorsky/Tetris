using UnityEngine;

public class BlitzEndStrategy : IEndStrategy
{
    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IPieceController pieceController)
    {
        pieceController.OnSpawnBlocked += gameEndController.GameDefeat;

        gameScore.OnTimeOver += gameEndController.GameEnd;
    }
}
