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

    void Awake()
    {
        _gameComposition = new GameComposition(GlobalServices.TicketManager, GlobalServices.LoadManager, GlobalServices.GameDataManager, gameConfig, GlobalServices.Resources);

        _gameComposition.CreateUpdateOrder();

        _bagView.Construct(_gameComposition.Bag, _gameComposition.TicketManager);
        _holdPieceView.Construct(_gameComposition.PieceController, _gameComposition.TicketManager);
        _pauseManagerView.Construct(_gameComposition.GameStateMachine);
        _tetrisFieldView.Construct(_gameComposition.PieceController, _gameComposition.TetrisField, _gameComposition.PlayerInput, _gameComposition.TicketManager, _gameComposition.GameResources);
        _gameScoreView.Construct(_gameComposition.GameScore);
        _gameEndView.Construct(_gameComposition.GameEndController);

        _menuOrRestart.Construct(_gameComposition.PauseController, _gameComposition.LoadManager);
    }

    private void Start()
    {
        _startText.gameObject.SetActive(false);

        _startCanvas.gameObject.SetActive(true);

        _backGround.sprite = _gameComposition.GameResources.Backgrounds[Random.Range(0, _gameComposition.GameResources.Backgrounds.Count)];

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

        _gameComposition.GameStateMachine.ChangeState(GameState.Gameplay);
    }
}
