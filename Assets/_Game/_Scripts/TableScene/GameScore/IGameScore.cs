using System;
using UnityEngine;

public interface IGameScore 
{
    public event Action<float, int, int> OnAfterUpdate;
    public event Action<ComboType, int> OnComboUpdate;

    public event Action<int, int, float> OnDefeat;
    public event Action<int, int, float> OnGameEnd;
}
