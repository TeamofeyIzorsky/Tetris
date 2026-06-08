using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        G.GResources = Resources.Load<GameResources>("GameResources");

        GameObject faderObject = Instantiate(G.GResources.FaderPrefab);
        DontDestroyOnLoad(faderObject);
        Fader fader = faderObject.GetComponent<Fader>();


        GameObject loadManagerGameObject = new GameObject("LoadManager Object");
        DontDestroyOnLoad(loadManagerGameObject);
        LoadManager loadManager = loadManagerGameObject.AddComponent<LoadManager>();
        loadManager.Init(fader);
        G.LoadManager = loadManager;

        GameObject DataManagerGameObject = new GameObject("DataManager Object");
        DontDestroyOnLoad(DataManagerGameObject);
        DataManager dataManager = DataManagerGameObject.AddComponent<DataManager>();
        G.DataManager = dataManager;

        GameObject playerInputObject = new GameObject("playerInput Object");
        DontDestroyOnLoad(playerInputObject);
        PlayerInput playerInput = playerInputObject.AddComponent<OldInputSystem>();

        G.PlayerInput = playerInput;

        G.LoadManager.Load(new LoadSettings()
        {
            needFade = false,
            sceneName = "MainMenu"
        });
    }
}
