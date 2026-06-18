using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TableBoot : MonoBehaviour
{
    //Этот класс является точкой входа на игровом поле. Он иницилизирует системы и стартовую анимацию
    [Header("Game Config")]
    [SerializeField] private GameConfigSO gameConfig;

    [Header("View")]
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

    private GameResourcesSO _gameResources;

    void Awake()
    {
        GameTicket gameTicket = GlobalServices.TicketManager.GetGameTicket();

        _gameResources = GlobalServices.Resources;

        ThemeSO theme = gameTicket.Theme;
        GameMode gameMode = gameTicket.GameMode;

        _gameComposition = new GameComposition(gameConfig, gameMode, theme);

        _gameComposition.CreateUpdateOrder();

        _bagView.Construct(_gameComposition.Bag, theme);
        _holdPieceView.Construct(_gameComposition.GameManager, theme);
        _pauseManagerView.Construct(_gameComposition.PauseController);
        _tetrisFieldView.Construct(_gameComposition.GameManager, _gameComposition.PlayerInput, theme, _gameResources.BlockPrefab);
        _gameScoreView.Construct(_gameComposition.GameScore);
        _gameEndView.Construct(_gameComposition.EndGameManager);

        _menuOrRestart.Construct(_gameComposition.PauseController);

        _gameComposition.PauseController.Pause(true);
    }

    private void Start()
    {
        _startText.gameObject.SetActive(false);

        _startCanvas.gameObject.SetActive(true);

        _backGround.sprite = _gameResources.Backgrounds[Random.Range(0, _gameResources.Backgrounds.Count)];

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
