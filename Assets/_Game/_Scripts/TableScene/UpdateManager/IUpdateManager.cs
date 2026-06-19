using UnityEngine;

public interface IUpdateManager
{
    public void Add(IUpdatable updatable);
    public void Remove(IUpdatable updatable);
}
