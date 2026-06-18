using UnityEngine;

public class GameTicket
{
    public GameMode GameMode { get; private set; }
    public ThemeSO Theme { get; private set; }

    public GameTicket(GameMode gameMode, ThemeSO theme)
    {
        GameMode = gameMode;
        Theme = theme;
    }
}
