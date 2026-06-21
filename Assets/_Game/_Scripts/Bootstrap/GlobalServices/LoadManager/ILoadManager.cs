public interface ILoadManager
{
    public void Load(LoadSettings loadSettings);

}

public struct LoadSettings
{
    public LoadSettings(string sceneName, int? sceneNum, bool needFade)
    {
        SceneName = sceneName;
        SceneNum = sceneNum;
        NeedFade = needFade;
    }

    public string SceneName;
    public int? SceneNum;
    public bool NeedFade;
}

