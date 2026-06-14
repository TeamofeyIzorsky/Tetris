using UnityEngine;

/*public interface IPausableUpdate
{
    void PausableUpdate();
}*/


public abstract class PausableBehaviour : MonoBehaviour
{
    //Классы наследники обновляются только, если нет паузы

    private void Update()
    {
        if (!G.PauseManager.GetIsPauseStatus())
        {
            PausableUpdate();
        }
    }

    protected abstract void PausableUpdate();
}