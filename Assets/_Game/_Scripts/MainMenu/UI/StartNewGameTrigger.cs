using UnityEngine;

public class StartNewGameTrigger : MonoBehaviour
{
    //UI кнопки вызывают StartNewGame для начала игры

    private ILoadManager _loadManager;
    private ITicketManager _ticketManager;

    private GameResourcesSO _gameResources;

    public void Construct(ILoadManager loadManager, ITicketManager ticketManager, GameResourcesSO gameResources)
    {
        _loadManager = loadManager;
        _ticketManager = ticketManager;

        _gameResources = gameResources;
    }


    public void StartNewGame(string mode)
    {
        switch (mode)
        {
            case "Blitz":
                _ticketManager.SetGameTicket(new GameTicket(GameMode.Blitz, _gameResources.ThemeSOs[0]));
                break;
            case "Standart":
                _ticketManager.SetGameTicket(new GameTicket(GameMode.Standard, _gameResources.ThemeSOs[0]));
                break;
            case "40Lines":
                _ticketManager.SetGameTicket(new GameTicket(GameMode.Lines40, _gameResources.ThemeSOs[0]));
                break;
            default:
                //NONE
                return;

        }

        _loadManager.Load(new LoadSettings()
        {
            NeedFade = true,
            SceneName = "TableScene"
        });
    }
}
