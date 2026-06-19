using UnityEngine;

public class BlitzEndStrategy : IEndStrategy
{
    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IGameManager gameManager)
    {
        gameManager.OnSpawnBlocked += gameEndController.GameDefeat;

        gameScore.OnTimeOver += gameEndController.GameEnd;
    }
}
