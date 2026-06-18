using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    //Отображает экран паузы

    [SerializeField] private Canvas _pauseCanvas;

    public void Construct(IPauseController pauseController)
    {
        pauseController.OnChangePauseStatus += UpdatePauseView;
    }

    private void Start()
    {
        _pauseCanvas.enabled = false;

    }

    
    private void UpdatePauseView(bool pause)
    {
        _pauseCanvas.enabled = pause;

        Cursor.visible = pause;
    }
}
