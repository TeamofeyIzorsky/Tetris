using UnityEngine;

public class MenuOrRestart : MonoBehaviour
{
    public void Restart()
    {
        G.LoadManager.Load(new LoadSettings()
        {
            needFade = true,
            sceneName = "TableScene"
        });
    }

    public void Exit()
    {
        G.LoadManager.Load(new LoadSettings()
        {
            needFade = true,
            sceneName = "MainMenu"
        });
    }
}
