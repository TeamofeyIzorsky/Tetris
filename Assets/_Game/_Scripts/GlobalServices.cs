using UnityEngine;

public class GlobalServices : MonoBehaviour
{
    public static ILoadManager LoadManager {  get; private set; }

    public static void Register(ILoadManager loadManager)
    {
        LoadManager = loadManager;
    }
}
