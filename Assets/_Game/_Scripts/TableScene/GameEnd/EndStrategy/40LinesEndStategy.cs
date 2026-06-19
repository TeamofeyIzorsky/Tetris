using UnityEngine;

public class Lines40EndStategy: IEndStrategy
{
    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IGameManager gameManager)
    {
        gameManager.OnSpawnBlocked += gameEndController.GameDefeat;

        gameScore.On40Lines += gameEndController.GameEnd;
    }
}
