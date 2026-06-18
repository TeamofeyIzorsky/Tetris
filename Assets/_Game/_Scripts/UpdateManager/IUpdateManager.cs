using UnityEngine;

public interface IUpdateManager
{
    public bool IsPaused { get; }

    public void Pause(bool pause);
    public void Add(IUpdatable updatable);
    public void Remove(IUpdatable updatable);
}
