using System;
using UnityEngine;

public interface IGameScore : IPauseUpdatable
{
    public RoundData GetRoundData();

    public event Action OnTimeOver;
    public event Action On40Lines;

    public event Action<RoundData> OnAfterUpdate;
    public event Action<ComboType, int> OnComboUpdate;
}
