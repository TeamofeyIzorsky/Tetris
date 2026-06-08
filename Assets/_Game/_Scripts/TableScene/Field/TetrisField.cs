using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class TetrisField : PausableBehaviour, ITetrisField
{
    //Основной класс, отвечающий за игровое поле и управление игрой

    //Настройки игрового поля
    [Header("Down")]
    [SerializeField] private float _timeForDown = 0.25f;
    [SerializeField] private float _lockDelay = 0.5f;

    [SerializeField] private List<SpeedLevel> _speedLevels;

    [Header("Fast Down Move")]
    [SerializeField] private float _timeForFastDown = 0.05f;

    [Header("Horizontal Move")]
    [SerializeField] private float _timeForStartHorizontalMove = 0.2f;
    [SerializeField] private float _timeForFastHorizontalMove = 0.05f;


    //Grid Settings
    public const int HEIGHT = 24;
    public const int WIDTH = 10;

    //TODO: надо зменить эвентами
    //[SerializeField] private NextBagView _nextBagView;
    //[SerializeField] private NextPieceBag _holdPieceView;

    private Cell[,] _grid;

    //Bag and current piece
    private List<Piece> _currentBag = new List<Piece>();
    private Piece _currentPiece;

    //Hold
    private Piece _holdPiece;
    private bool _isCanHold = true;

    //Piece positions
    private List<Vector2Int> _piecePositions = new List<Vector2Int>();
    private Vector2Int _matrixPivotPosition = new Vector2Int();

    private List<Vector2Int> _finalPositions = new List<Vector2Int>();


    //Down
    private float _downTimer = 0;
    private float _downMoveTimer = 0;

    private int _currentSpeedLevel = 0;
    private int _destroyedLines = 0;


    private const int MAX_RESET_LOCK_DELAY_COUNT = 15;

    private int _resetLockDelayCounter;
    private bool _isAnyMove;


    //Horizontal
    private float _horizontalTimer = 0;
    private int _horizontalMovesCount = 0;

    private Vector2Int _prevDirection = new Vector2Int();


    //Events
    public event Action<IReadOnlyList<Vector2Int>, Cell[,], Piece, IReadOnlyList<Vector2Int>> OnAfterUpdate;
    public event Action<IReadOnlyList<Piece>> OnUpdateBag;
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action<int, int> OnDeleteLinesEnd;
    public event Action OnGameOver;

    private void Awake()
    {
        G.TetrisField = this;
    }

    private void Start()
    {
        _grid = new Cell[WIDTH, HEIGHT];

        //Создаем первую фигуру
        CreatePieceFromBag();

        //Делаем первый апдейт сразу, чтобы фигура успела появится на экране до того, как игра начнется, чтобы игрок её сразу видел.
        PausableUpdate();
    }

    public override void PausableUpdate()
    {
        Down();

        _isAnyMove = false;

        FoundFinalPositions();

        if (G.PlayerInput.HardDown)
        {
            //Мгновенный спуск фигуры

            _downTimer = 0;

            Move(_finalPositions);

            OnAfterUpdate?.Invoke(_piecePositions, _grid, _currentPiece, _finalPositions);
            return;
        }
        else if (G.PlayerInput.HoldPiece)
        {
            //Холдим фигуру в карман

            PlayerHold();

            OnAfterUpdate?.Invoke(_piecePositions, _grid, _currentPiece, _finalPositions);

            return;
        }

        if (G.PlayerInput.Down || G.PlayerInput.DownHold)
        {
            //Ускоренный спуск вниз

            PlayerMoveDown();
        }

        if (G.PlayerInput.Left || G.PlayerInput.LeftHold)
        {
            //Влыво

            PlayerMoveHorizontal(Vector2Int.left);
        }

        else if (G.PlayerInput.Right || G.PlayerInput.RightHold)
        {
            //Вправо

            PlayerMoveHorizontal(Vector2Int.right);
        }

        else if (G.PlayerInput.Rotate)
        {
            //Поворот фигуры
            PlayerRotatePiece();
        }

        OnAfterUpdate?.Invoke(_piecePositions, _grid, _currentPiece, _finalPositions);
    }

    #region Create Piece and Bag
    //Создаем новую фигуру на поле из бэга
    private void CreatePieceFromBag()
    {
        if (_currentBag.Count < 7)
        {
            ExpandBag();
        }

        _matrixPivotPosition = new Vector2Int(3, 19);

        _currentPiece = _currentBag[0];
        _currentBag.Remove(_currentPiece);

        OnUpdateBag?.Invoke(_currentBag);

        //_nextBagView.UpdateBag();

        List<Vector2Int> positions = _currentPiece.GetPositions(_matrixPivotPosition);

        if (!CheckPositions(positions))
        {
            Debug.Log("GAMEOVER!");

            OnGameOver?.Invoke();

            return;
        }

        _piecePositions = positions;

        //UpdateBagView();
    }

    //Расширяем бэг, если он кончается
    private void ExpandBag()
    {
        Piece[] bag = new Piece[]
        {
            new OBlock(),
            new ZBlock(),
            new SBlock(),
            new LBlock(),
            new JBlock(),
            new IBlock(),
            new TBlock(),
        };

        for (int i = 0; i < bag.Length; i++)
        {
            Piece piece = bag[i];

            int randIndex = Random.Range(0, 7);
            bag[i] = bag[randIndex];
            bag[randIndex] = piece;
        }

        foreach (Piece piece in bag)
        {
            _currentBag.Add(piece);
        }
    }

    #endregion


    #region Place Piece
    //Устанавливаем фигуру
    private void Place()
    {
        //Debug.Log("PLACE!");

        foreach (var piece in _piecePositions)
        {
            _grid[piece.x, piece.y].Occupied = true;

            _grid[piece.x, piece.y].Color = _currentPiece.color;
        }

        _piecePositions.Clear();

        FoundAndDeleteFillLines();

        _isCanHold = true;

        CreatePieceFromBag();

        OnUpdateHoldPiece?.Invoke(_holdPiece, _isCanHold);
    }

    //После установки удаляем полные линии и сдвигаем верхние линии вниз
    private void FoundAndDeleteFillLines()
    {
        int deleteCount = 0;


        for (int y = HEIGHT - 1; y >= 0; y--)
        {
            bool flagLine = true;

            for (int x = 0; x < WIDTH; x++)
            {
                if (!_grid[x, y].Occupied)
                {
                    flagLine = false;
                }
            }

            if (flagLine)
            {
                _destroyedLines++;
                deleteCount++;

                for (int x = 0; x < WIDTH; x++)
                {
                    _grid[x, y].Occupied = false;
                }

                for (int iy = y; iy < 22; iy++)
                {
                    for (int ix = 0; ix < WIDTH; ix++)
                    {
                        _grid[ix, iy] = _grid[ix, iy + 1];

                    }
                }
            }
        }

        if(deleteCount > 0)
        {
            OnDeleteLinesEnd?.Invoke(deleteCount, _destroyedLines);

            CheckSpeedLevel();
        }
    }
    #endregion


    #region PlayerActions
    //Игрок делает поворот фигуры
    private void PlayerRotatePiece()
    {
        _currentPiece.Rotate();

        List<Vector2Int> positions = _currentPiece.GetPositions(_matrixPivotPosition);

        bool flag = CheckPositions(positions);

        if (flag)
        {
            _piecePositions = positions;
            return;
        }

        List<Vector2Int> tryPos = new List<Vector2Int>();

        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.left);
        }

        flag = CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;

            _piecePositions = tryPos;
            return;
        }

        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.left);
        }

        flag = CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;

            _piecePositions = tryPos;
            return;
        }

        tryPos.Clear();

        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.right);
        }

        flag = CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;

            _piecePositions = tryPos;
            return;
        }

        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.right);
        }

        if (flag)
        {
            _isAnyMove = true;

            _piecePositions = tryPos;
            return;
        }

        _currentPiece.UndoRotate();
    }

    //Игрок холдит фигуру
    private void PlayerHold()
    {
        if (!_isCanHold) return;

        _isCanHold = false;

        if (_holdPiece != null)
        {
            _holdPiece.currentRotate = 0;

            _currentBag.Insert(0, _holdPiece);
        }

        _holdPiece = _currentPiece;

        //_holdPieceView.ShowPiece(_holdPiece);
        OnUpdateHoldPiece?.Invoke(_holdPiece, _isCanHold);

        _downTimer = 0;

        CreatePieceFromBag();
    }

    //Игрок двигает фигуру вниз быстрее
    private void PlayerMoveDown()
    {
        if (G.PlayerInput.Down)
        {
            _downTimer = 0;

            Move(Vector2Int.down);
        }
        else if (G.PlayerInput.DownHold)
        {
            _downTimer = 0;

            _downMoveTimer += Time.deltaTime;

            if (_downMoveTimer >= _timeForFastDown)
            {
                _downMoveTimer = 0;

                Move(Vector2Int.down);
            }
        }
    }

    //Игрок двигает фигуру горизонтально
    private void PlayerMoveHorizontal(Vector2Int direction)
    {
        if ((G.PlayerInput.Left || G.PlayerInput.Right) || direction != _prevDirection)
        {
            _horizontalMovesCount = 1;

            _horizontalTimer = 0;

            _prevDirection = direction;

            Move(direction);

            //Debug.Log("Slow!");
        }
        else if ((G.PlayerInput.LeftHold || G.PlayerInput.RightHold) && _prevDirection == direction)
        {

            _horizontalTimer += Time.deltaTime;

            if (_horizontalMovesCount >= 2)
            {

                if (_horizontalTimer >= _timeForFastHorizontalMove)
                {
                    //Debug.Log("Fast!");

                    //Debug.Log(_horizontalTimer);

                    _horizontalTimer = 0;

                    _horizontalMovesCount++;

                    Move(direction);

                    _prevDirection = direction;
                }
            }
            else
            {

                if (_horizontalTimer >= _timeForStartHorizontalMove)
                {
                    //Debug.Log("Slow!");

                    _horizontalTimer = 0;

                    _horizontalMovesCount++;

                    Move(direction);

                    _prevDirection = direction;
                }
            }
        }
    }

    #endregion

    //Автоматическое опускание фигуры после истечения таймера
    private void Down()
    {
        _downTimer += Time.deltaTime;

        List<Vector2Int> newPositions = new List<Vector2Int>();

        foreach(var piecePosition in _piecePositions)
        {
            newPositions.Add(piecePosition - new Vector2Int(0, 1));
        }

        bool isOnGround = !CheckPositions(newPositions);

        if(!isOnGround)
        {
            _resetLockDelayCounter = 0;
        }
        else if (isOnGround && _resetLockDelayCounter < MAX_RESET_LOCK_DELAY_COUNT && _isAnyMove)
        {
            _resetLockDelayCounter++;

            _downTimer = 0;
        }

        if(!isOnGround && _downTimer >= _timeForDown)
        {
            _resetLockDelayCounter = 0;

            _downTimer = 0;
            _piecePositions = newPositions;

            _matrixPivotPosition = new Vector2Int(_matrixPivotPosition.x, _matrixPivotPosition.y - 1);
        }
        else if(isOnGround && _downTimer >= _lockDelay)
        {
            _downTimer = 0;

            _resetLockDelayCounter = 0;

            Place();
        }

    }

    //Вспомогательный метод для обработки движения
    private bool Move(Vector2Int moveVector)
    {
        List<Vector2Int> newPositions = new List<Vector2Int>();


        foreach (Vector2Int position in _piecePositions)
        {
            Vector2Int newPosition = new Vector2Int(position.x + moveVector.x, position.y + moveVector.y);

            newPositions.Add(newPosition);
        }

        bool flag = CheckPositions(newPositions);

        if (flag)
        {
            _isAnyMove = true;

            _piecePositions = newPositions;

            _matrixPivotPosition = new Vector2Int(_matrixPivotPosition.x + moveVector.x, _matrixPivotPosition.y + moveVector.y);
        }
        else if (moveVector == Vector2Int.down)
        {
            Place();
        }

        return flag;
    }
    //Переопределенный выше метод
    private bool Move(List<Vector2Int> finalPosition)
    {
        if (finalPosition.Count == 0)
        {
            return false;
        }

        _piecePositions.Clear();
        _piecePositions = finalPosition;

        Place();

        return true;
    }

    //Проверяем не должны ли мы ускорить игру
    private void CheckSpeedLevel()
    {
        SpeedLevel speedLevel = _speedLevels[_currentSpeedLevel];

        if(_destroyedLines >= speedLevel.LinesCount)
        {
            if(_currentSpeedLevel < _speedLevels.Count)
            {
                _currentSpeedLevel++;
            }

            _timeForDown = speedLevel.Speed;
        }
    }

    //Воображаемо опускаем фигуру вниз по данным координатам, пока она не упрется, чтобы найти финальную позицию на которую она упадет, если её не двигать или мгновенно отправить вниз
    private void FoundFinalPositions()
    {
        List<Vector2Int> prevPositions = new();

        for (int i = 0; i <= _matrixPivotPosition.y + 2; i++)
        {
            List<Vector2Int> positions = new();

            foreach (Vector2Int position in _piecePositions)
            {
                positions.Add(new Vector2Int(position.x, position.y - i));
            }

            if (!CheckPositions(positions) && prevPositions.Count > 0)
            {
                //Debug.Log("FOUND!");

                _finalPositions.Clear();

                _finalPositions.AddRange(prevPositions);

                return;
            }

            prevPositions = positions;
        }
    }

    #region Utilities
    //Всопомогательный метод для проверки позиций фигуры
    private bool CheckPositions(List<Vector2Int> positions)
    {
        bool isCorrectPositions = true;

        foreach (Vector2Int position in positions)
        {
            if (position.y < 0 || position.x > 9 || position.x < 0 || _grid[position.x, position.y].Occupied)
            {
                isCorrectPositions = false;
                break;
            }
        }

        return isCorrectPositions;
    }

    #endregion
}

