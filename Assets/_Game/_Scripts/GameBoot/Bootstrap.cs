using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        GameResourcesSO gameResources = Resources.Load<GameResourcesSO>("GameResources");

        GameObject faderObject = Instantiate(gameResources.FaderPrefab);
        DontDestroyOnLoad(faderObject);
        Fader fader = faderObject.GetComponent<Fader>();


        GameObject loadManagerGameObject = new GameObject("LoadManager Object");
        DontDestroyOnLoad(loadManagerGameObject);
        LoadManager loadManager = loadManagerGameObject.AddComponent<LoadManager>();
        loadManager.Construct(fader);


        GameObject DataManagerGameObject = new GameObject("DataManager Object");
        DontDestroyOnLoad(DataManagerGameObject);
        DataManager dataManager = DataManagerGameObject.AddComponent<DataManager>();
        G.DataManager = dataManager;


        G.DataManager.DeserializeSave();

        TicketManager ticketManager = new();

        GlobalServices.Register(loadManager, ticketManager, gameResources);

        loadManager.Load(new LoadSettings()
        {
            NeedFade = false,
            SceneName = "MainMenu"
        });
    }
}
