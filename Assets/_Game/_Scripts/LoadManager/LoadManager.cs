using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour, ILoadManager
{
    private Fader _fader;

    public void Construct(Fader fader)
    {
        _fader = fader;
    }

    private bool _loadingIsNow;


    public void Load(LoadSettings loadSettings)
    {
        if (_loadingIsNow == true)
        {
            Debug.LogError("Загрузка уже начата!");
            return;
        }

        if (loadSettings.NeedFade == true)
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

        if (loadSettings.SceneNum != null)
        {
            SceneManager.LoadScene((int)loadSettings.SceneNum);
        }
        else
        {
            SceneManager.LoadScene(loadSettings.SceneName);
        }

        _loadingIsNow = false;

        yield return null;
    }

    private IEnumerator LoadingFade(LoadSettings loadSettings)
    {

        _loadingIsNow = true;

        Animation animation = _fader.FadeClose();

        var state = animation["FadeClose"];

        while (animation.isPlaying)
        {
            yield return null;
        }

        AsyncOperation progress = null;


        if (loadSettings.SceneNum != null)
        {
            progress = SceneManager.LoadSceneAsync((int)loadSettings.SceneNum);
        }
        else
        {
            progress = SceneManager.LoadSceneAsync(loadSettings.SceneName);
        }

        while (!progress.isDone)
        {
            yield return null;
        }

        yield return null;
        yield return null;
        yield return null;

        animation = _fader.FadeOpen();

        state = animation["FadeOpen"];

        while (animation.isPlaying)
        {

            yield return null;
        }

        _loadingIsNow = false;

        yield return null;
    }
}
