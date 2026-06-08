using UnityEngine;

public interface IPausableUpdate
{
    void PausableUpdate();
}

public abstract class PausableBehaviour : MonoBehaviour, IPausableUpdate
{

    private void Update()
    {
        if (!G.PauseManager.GetIsPauseStatus())
        {
            PausableUpdate();
        }
    }

    public abstract void PausableUpdate();
}