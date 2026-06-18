using UnityEngine;

public class GlobalServices : MonoBehaviour
{
    public static GameResourcesSO Resources {  get; private set; }
    public static ILoadManager LoadManager {  get; private set; }
    public static ITicketManager TicketManager { get; private set; }

    public static void Register(ILoadManager loadManager, ITicketManager ticketManager, GameResourcesSO gameResources)
    {
        Resources = gameResources;
        LoadManager = loadManager;
        TicketManager = ticketManager;
    }
}
