using UnityEngine;

public class MenuOrRestart : MonoBehaviour
{
    //UI Кнопки вызывают методы класса для возвращения в меню или рестарта
    private IPauseController _pauseController;
    private ILoadManager _loadManager;

    public void Construct(IPauseController pauseController, ILoadManager loadManager)
    {
        _pauseController = pauseController;
        _loadManager = loadManager;
    }


    public void Restart()
    {
        _loadManager.Load(new LoadSettings()
        {
            NeedFade = true,
            SceneName = "TableScene"
        });
    }

    public void Resume()
    {
        _pauseController.Pause(false);
    }

    public void Exit()
    {
        _loadManager.Load(new LoadSettings()
        {
            NeedFade = true,
            SceneName = "MainMenu"
        });
    }
}
