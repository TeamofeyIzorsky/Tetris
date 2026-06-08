using System.Collections.Generic;
using UnityEngine;

public class GameResources : ScriptableObject
{
    public float StartWaitTime = 3f;

    public GameObject BlockPrefab;
    public GameObject FaderPrefab;

    public List<Sprite> Backgrounds;
}
