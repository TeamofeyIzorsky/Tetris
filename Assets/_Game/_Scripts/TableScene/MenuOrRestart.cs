using UnityEngine;

public class MenuOrRestart : MonoBehaviour
{
    //UI Кнопки вызывают методы класса для возвращения в меню или рестарта
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
