using UnityEngine;

public interface IGameDataManager
{
    public void RoundEnd(RoundData roundData);

    public GameData GetGameData();
}
