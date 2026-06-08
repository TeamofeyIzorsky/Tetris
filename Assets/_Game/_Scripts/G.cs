using UnityEngine;

public static class G
{
    //Глобальные Системы
    public static LoadManager LoadManager;
    public static GameResources GResources;
    public static DataManager DataManager;
    public static PlayerInput PlayerInput;

    //Геймплейные системы
    public static ITetrisField TetrisField;
    public static IGameScore GameScore;
    public static IEndGameManager GameEndManager;
    public static IPauseManager PauseManager;


    public static GameMode GameMode = GameMode.Standart;
}

//Текуший игровой режим
public enum GameMode
{
    None,
    Standart,
    Casual,
    Lines40,
    Blitz,
}