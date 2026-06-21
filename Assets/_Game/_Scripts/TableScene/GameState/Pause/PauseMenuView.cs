using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    //Отображает экран паузы

    [SerializeField] private Canvas _pauseCanvas;

    public void Construct(IGameStateMachine gameStateMachine)
    {
        gameStateMachine.OnStateChanged += UpdatePauseView;
    }

    private void Start()
    {
        _pauseCanvas.enabled = false;

    }


    private void UpdatePauseView(GameState gameState)
    {
        bool pause = gameState == GameState.Paused;
        
        _pauseCanvas.enabled = pause;

        Cursor.visible = pause;
    }
}
