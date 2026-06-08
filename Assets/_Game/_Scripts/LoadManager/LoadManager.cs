using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{
    //public static LoadManager Instance;
    private static Fader _fader;

    private bool _loadingIsNow;

    public LoadSettings LoadSettings = new();

    public void Init(Fader fader)
    {
        _fader = fader;
    }


    public void Load(LoadSettings loadSettings)
    {
        if (_loadingIsNow == true)
        {
            Debug.LogError("Загрузка уже начата!");
            return;
        }

        if (loadSettings.needFade == true)
        {
            StartCoroutine(LoadingFade(loadSettings));
        }
        else
        {
            StartCoroutine(Loading(loadSettings));
        }
    }

    private IEnumerator Loading(LoadSettings loadSettings)
    {
        _loadingIsNow = true;

        LoadSettings = loadSettings;

        if (loadSettings.sceneNum != null)
        {
            SceneManager.LoadScene((int)loadSettings.sceneNum);
        }
        else
        {
            SceneManager.LoadScene(loadSettings.sceneName);
        }

        _loadingIsNow = false;

        yield return null;
    }

    private IEnumerator LoadingFade(LoadSettings loadSettings)
    {
        LoadSettings = loadSettings;

        _loadingIsNow = true;

        Animation animation = _fader.FadeClose();

        var state = animation["FadeClose"];

        while (animation.isPlaying)
        {
            yield return null;
        }

        AsyncOperation progress = null;


        if (loadSettings.sceneNum != null)
        {
            progress = SceneManager.LoadSceneAsync((int)loadSettings.sceneNum);
        }
        else
        {
            progress = SceneManager.LoadSceneAsync(loadSettings.sceneName);
        }

        while (!progress.isDone)
        {
            yield return null;
        }


        yield return null;
        yield return null;
        yield return null;

        //if (loadSettings.save == SaveSetting.NeedAfterLoad) G.DataManager.Save(SaveLoadMode.CurrentSave); //, null); LoadManager не должен заниматься сохранениями

        animation = _fader.FadeOpen();

        state = animation["FadeOpen"];

        //AudioManager.Instance.SetPitch(1f, true);

        while (animation.isPlaying)
        {
            //if (state.normalizedTime > 0.25f && G.SceneStart == false)
            //{
               //AudioManager.ResetVolume();

                //G.SceneStart = true;
            //}

            yield return null;

            //Debug.Log("Yeld");
        }


        _loadingIsNow = false;


        yield return null;
    }
}

public enum SceneType
{
    Scene,
    Level,
    BattleScene,
}

public class LoadSettings
{
    public string sceneName;
    public int? sceneNum = null;
    public bool needFade = true;
}

