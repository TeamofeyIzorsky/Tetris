using System.Collections.Generic;
using UnityEngine;

public class GameResources : ScriptableObject
{
    //Хранит в себе ресурсы для других систем и конфиг игры

    [Header("Down")]
    [SerializeField] public float TimeForDown = 0.25f;
    [SerializeField] public float LockDelay = 0.5f;

    [SerializeField] public List<SpeedLevel> SpeedLevels;

    [Header("Fast Down Move")]
    [SerializeField] public float TimeForFastDown = 0.05f;

    [Header("Horizontal Move")]
    [SerializeField] public float TimeForStartHorizontalMove = 0.2f;
    [SerializeField] public float TimeForFastHorizontalMove = 0.05f;

    public float StartWaitTime = 3f;

    public GameObject BlockPrefab;
    public GameObject FaderPrefab;

    public List<Sprite> Backgrounds;

    public List<ThemeSO> ThemeSOs;
}
