using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    //Иницилиазация глобальных систем игры


    void Start()
    {
        GameResourcesSO gameResources = Resources.Load<GameResourcesSO>("GameResources");

        GameObject faderObject = Instantiate(gameResources.FaderPrefab);
        DontDestroyOnLoad(faderObject);
        Fader fader = faderObject.GetComponent<Fader>();


        GameObject loadManagerGameObject = new GameObject("Load Manager Object");
        DontDestroyOnLoad(loadManagerGameObject);
        LoadManager loadManager = loadManagerGameObject.AddComponent<LoadManager>();
        loadManager.Construct(fader);

        ISaveManager saveManager = new SaveManager();

        GameObject GameDataManagerGameObject = new GameObject("Game Data Manager Object");
        DontDestroyOnLoad(GameDataManagerGameObject);
        IGameDataManager gameDataManager = GameDataManagerGameObject.AddComponent<GameDataManager>().Construct(saveManager);

        ITicketManager ticketManager = new TicketManager();

        GlobalServices.Register(loadManager, ticketManager, gameDataManager, gameResources);

        GlobalServices.LoadManager.Load(new LoadSettings()
        {
            NeedFade = false,
            SceneName = "MainMenu"
        });
    }
}
