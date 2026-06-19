using System;
using UnityEngine;

public interface IGameEndController
{
    public void GameDefeat();
    public void GameEnd();

    public event Action<GameData, RoundData> OnGameEnded;
}
