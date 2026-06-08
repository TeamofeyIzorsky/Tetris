using UnityEngine;

public class StartNewGameTrigger : MonoBehaviour
{
    public void StartNewGame(string mode)
    {
        switch (mode)
        {
            case "Blitz":
                G.GameMode = GameMode.Blitz;
                break;
            case "Standart":
                G.GameMode = GameMode.Standart;
                break;
            case "40Lines":
                G.GameMode = GameMode.Lines40;
                break;
            default:
                G.GameMode = GameMode.None;
                return;

        }

        G.LoadManager.Load(new LoadSettings()
        {
            needFade = true,
            sceneName = "TableScene"
        });
    }
}
