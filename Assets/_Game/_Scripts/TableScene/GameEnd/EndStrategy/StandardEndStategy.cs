using UnityEngine;

public class StandardEndStategy : IEndStrategy
{
    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IGameManager gameManager)
    {
        gameManager.OnSpawnBlocked += gameEndController.GameEnd;

    }
}
