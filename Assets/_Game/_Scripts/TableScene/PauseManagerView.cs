using UnityEngine;

public class PauseManagerView : MonoBehaviour
{
    [SerializeField] private Canvas _pauseCanvas;

    private void Start()
    {
        _pauseCanvas.enabled = false;

        G.PauseManager.OnChangePauseStatus += UpdatePauseView;
    }

    
    private void UpdatePauseView(bool pause)
    {
        _pauseCanvas.enabled = pause;
    }
}
