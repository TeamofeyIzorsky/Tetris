using UnityEngine;

public class GlobalServices : MonoBehaviour
{
    //Хранит в себе глобальные системы для других Composition Root

    public static GameResourcesSO Resources {  get; private set; }
    public static ILoadManager LoadManager {  get; private set; }
    public static ITicketManager TicketManager { get; private set; }
    public static IGameDataManager GameDataManager { get; private set; }

    public static void Register(ILoadManager loadManager, ITicketManager ticketManager, IGameDataManager gameDataManager, GameResourcesSO gameResources)
    {
        Resources = gameResources;
        LoadManager = loadManager;
        TicketManager = ticketManager;
        GameDataManager = gameDataManager;
    }
}
