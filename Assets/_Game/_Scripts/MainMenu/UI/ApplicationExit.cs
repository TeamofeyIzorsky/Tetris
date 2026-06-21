using UnityEngine;

public class ApplicationExit : MonoBehaviour
{
    //UI кнопка вызывает метод Exit для выхода из игры

    public void Exit()
    {
        Application.Quit();
    }
}
