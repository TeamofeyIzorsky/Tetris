using System.Collections.Generic;
using UnityEngine;

public class GameResourcesSO : ScriptableObject
{
    //Хранит в себе ресурсы для других систем

    public GameObject BlockPrefab;
    public GameObject FaderPrefab;

    public List<Sprite> Backgrounds;

    public List<ThemeSO> ThemeSOs;
}
