using UnityEngine;

public class MenuOrRestart : MonoBehaviour
{
    //UI Кнопки вызывают методы класса для возвращения в меню или рестарта
    private IPauseController _pauseController;

    public void Construct(IPauseController pauseController)
    {
        _pauseController = pauseController;
    }


    public void Restart()
    {
        GlobalServices.LoadManager.Load(new LoadSettings()
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
        GlobalServices.LoadManager.Load(new LoadSettings()
        {
            NeedFade = true,
            SceneName = "MainMenu"
        });
    }
}
