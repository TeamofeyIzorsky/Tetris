using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TableBoot : MonoBehaviour
{
    //Этот класс запускает сцену и стартовую анимацию


    [SerializeField] private SpriteRenderer _backGround;

    [Header("Start Canvas")]
    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private TMP_Text _startText;

    void Awake()
    {
        G.PauseManager.Pause(true);

        G.PlayerInput.SetActive(false);
    }

    private void Start()
    {

        _startText.gameObject.SetActive(false);

        _startCanvas.gameObject.SetActive(true);

        _backGround.sprite = G.GResources.Backgrounds[Random.Range(0, G.GResources.Backgrounds.Count)];

        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        _startText.gameObject.SetActive(true);

        var anim = DOTween.Sequence();

        _startText.text = "3";
        yield return _startText.transform.DOScale(3f, 0.75f).From(1f).WaitForCompletion();

        _startText.text = "2";
        yield return _startText.transform.DOScale(3f, 0.75f).From(1f).WaitForCompletion();

        _startText.text = "1";
        yield return _startText.transform.DOScale(3f, 0.75f).From(1f).WaitForCompletion();

        _startText.text = "START!";
        yield return _startText.transform.DOScale(2f, 0.5f).From(1.75f).WaitForCompletion();

        yield return new WaitForSeconds(0.25f);

        _startText.gameObject.SetActive(false);

        _startCanvas.gameObject.SetActive(false);

        G.PauseManager.Pause(false);

        G.PlayerInput.SetActive(true);
    }
}
