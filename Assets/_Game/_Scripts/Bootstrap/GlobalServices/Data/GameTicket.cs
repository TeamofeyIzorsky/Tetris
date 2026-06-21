using UnityEngine;

public struct GameTicket
{
    //Информация, которую мы хотим передать между сценами

    public GameMode GameMode { get; private set; }
    public ThemeSO Theme { get; private set; }

    public GameTicket(GameMode gameMode, ThemeSO theme)
    {
        GameMode = gameMode;
        Theme = theme;
    }
}
