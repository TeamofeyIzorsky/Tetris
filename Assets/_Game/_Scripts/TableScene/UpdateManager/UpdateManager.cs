using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateManager : MonoBehaviour, IUpdateManager
{
    private List<IUpdatable> _updatables = new();
    private List<IPauseUpdatable> _pauseUpdatables = new();

    private List<IUpdatable> _toAdd = new();
    private List<IUpdatable> _toRemove = new();

    public bool IsPaused { get => _isPaused; }

    private bool _isPaused;

    public void Pause(bool pause)
    {
        if (_isPaused == pause) return;

        _isPaused = pause;
    }

    public void Add(IUpdatable updatable)
    {
        if (updatable == null || _toAdd.Contains(updatable)) return;
        if(updatable is IPauseUpdatable)
        {
            if (_pauseUpdatables.Contains(updatable))
                return;
        }
        else
        {
            if (_updatables.Contains(updatable))
                return;
        }

        _toAdd.Add(updatable);
    }

    public void Remove(IUpdatable updatable)
    {
        _toRemove.Add(updatable);
    }

    public void Update()
    {
        float dt = Time.deltaTime;

        foreach(IUpdatable updatable in _updatables)
        {
            if (updatable == null)
            {
                _toRemove.Add(updatable);
                continue;
            }

            updatable.Tick(dt);
        }

        foreach(IPauseUpdatable pauseUpdatable in _pauseUpdatables)
        {
            if (pauseUpdatable == null)
            {
                _toRemove.Add(pauseUpdatable);
                continue;
            }

            if (_isPaused && pauseUpdatable.IsPausable) continue;

            pauseUpdatable.Tick(dt);
        }

        AddAll();
        RemoveAll();
    }

    private void AddAll()
    {
        if (_toAdd.Count == 0) return;

        foreach (var  updatable in _toAdd)
        {
            if (updatable == null)
            {
                continue;
            }

            if (updatable is IPauseUpdatable)
            {
                _pauseUpdatables.Add((IPauseUpdatable)updatable);
            }
            else
            {
                _updatables.Add(updatable);
            }
        }

        _toAdd.Clear();
    }

    private void RemoveAll()
    {
        if (_toRemove.Count == 0) return;

        foreach (var updatable in _toRemove)
        {
            if (updatable is IPauseUpdatable)
            {
                _pauseUpdatables.Remove((IPauseUpdatable)updatable);
            }
            else
            {
                _updatables.Remove(updatable);
            }
        }


        _toRemove.Clear();
    }
}
