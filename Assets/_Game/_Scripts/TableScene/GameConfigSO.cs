using System.Collections.Generic;
using UnityEngine;

public class GameConfigSO : ScriptableObject, IGameConfig
{
    //Хранит в себе данные скоростей и тему для данного раунда
    [Header("Down")]
    [SerializeField] private float _timeForDown;
    [SerializeField] private float _lockDelay;

    [Header("Fast Down Move")]
    [SerializeField] private float _timeForFastDown;

    [Header("Horizontal Move")]
    [SerializeField] private float _timeForStartHorizontalMove;
    [SerializeField] private float _timeForFastHorizontalMove;

    [SerializeField] private List<SpeedLevel> _speedLevels = new();


    public float TimeForDown { get => _timeForDown; }
    public float LockDelay { get => _lockDelay; }
    public float TimeForFastDown { get => _timeForFastDown; }

    public float TimeForStartHorizontalMove { get => _timeForStartHorizontalMove; }
    public float TimeForFastHorizontalMove { get => _timeForFastHorizontalMove; }

    public List<SpeedLevel> SpeedLevels { get => _speedLevels; } 

}
