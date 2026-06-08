using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    [SerializeField] private Animation _fadeOpen;
    [SerializeField] private Animation _fadeClose;

    private void Start()
    {
        OnLoad(new Scene(), LoadSceneMode.Single);

        _fadeClose.gameObject.SetActive(false);
        _fadeOpen.gameObject.SetActive(false);

        SceneManager.sceneLoaded += OnLoad;
    }

    private void OnLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public Animation FadeOpen()
    {
        _fadeClose.gameObject.SetActive(false);
        _fadeOpen.gameObject.SetActive(true);
        _fadeOpen.Play();

        return _fadeOpen;
    }

    public Animation FadeClose()
    {
        _fadeOpen.gameObject.SetActive(false);
        _fadeClose.gameObject.SetActive(true);
        _fadeClose.Play();

        return _fadeClose;
    }
}
