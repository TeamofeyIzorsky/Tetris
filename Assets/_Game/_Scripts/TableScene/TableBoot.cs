using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TableBoot : MonoBehaviour
{
    //Этот класс является точкой входа на игровом поле. Он иницилизирует системы и стартовую анимацию
    [SerializeField] private BagView _bagView;
    [SerializeField] private HoldPieceView _holdPieceView;
    [SerializeField] private PauseMenuView _pauseManagerView;
    [SerializeField] private FieldView _tetrisFieldView;
    [SerializeField] private ScoreView _gameScoreView;
    [SerializeField] private EndScreenView _gameEndView;

    [SerializeField] private MenuOrRestart _menuOrRestart;

    [SerializeField] private SpriteRenderer _backGround;

    [Header("Start Canvas")]
    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private TMP_Text _startText;


    private GameComposition _gameComposition;

    void Awake()
    {
        _gameComposition = new();

        _gameComposition.CreateUpdateOrder();

        _bagView.Construct(_gameComposition.Bag);
        _holdPieceView.Construct(_gameComposition.GameManager);
        _pauseManagerView.Construct(_gameComposition.PauseController);
        _tetrisFieldView.Construct(_gameComposition.GameManager, _gameComposition.PlayerInput);
        _gameScoreView.Construct(_gameComposition.GameScore);
        _gameEndView.Construct(_gameComposition.EndGameManager);

        _menuOrRestart.Construct(_gameComposition.PauseController);

        _gameComposition.PauseController.Pause(true);
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
        Cursor.visible = false;

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

        _gameComposition.PauseController.Pause(false);
    }
}
