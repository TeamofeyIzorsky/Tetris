using UnityEngine;

public class MenuComposition
{
    public IGameDataManager GameDataManager { get; private set;  }
    public ILoadManager LoadManager { get; private set; }
    public ITicketManager TicketManager { get; private set; }

    public GameResourcesSO GameResourcesSO { get; private set; }

    public MenuComposition(IGameDataManager gameDataManager, ILoadManager loadManager, ITicketManager ticketManager, GameResourcesSO gameResourcesSO)
    {
        GameDataManager = gameDataManager;
        LoadManager = loadManager;
        TicketManager = ticketManager;

        GameResourcesSO = gameResourcesSO;
    }
}
