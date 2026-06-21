using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : IPieceController
{
    private ITetrisField _tetrisField;
    private IGameParameters _gameParameters;
    private IBag _bag;

    public bool IsPausable { get => true; set => throw new System.NotImplementedException(); }

    public PieceController(IGameInputHandler gameInputHandler, ITetrisField tetrisField, IGameParameters gameParameters, IBag bag)
    {
        _gameParameters = gameParameters;
        _tetrisField = tetrisField;
        _bag = bag;

        gameInputHandler.OnHardDrop += PlacePiece;
        gameInputHandler.OnHold += PlayerHold;
        gameInputHandler.OnRotate += TryRotate;
        gameInputHandler.OnFastDown += FastDown;
        gameInputHandler.OnHorizonralMove += HorizonalMove;

        CreatePieceFromBag();
    }


    private Piece _currentPiece;
    private Piece _holdPiece;
    private bool _isCanHold = true;


    private float _downTimer = 0;


    //Задержка установки фигуры при касании земли
    private const int MAX_RESET_LOCK_DELAY_COUNT = 15;

    private int _resetLockDelayCounter;
    private bool _isAnyMove = false;


    public event Action<Piece, bool> OnUpdateHold;
    public event Action OnCreateNewPiece;
    public event Action OnSpawnBlocked;
    public event Action<Piece> PieceControllerTickOver;


    public void Tick(float deltaTime)
    {
        Down();

        PieceControllerTickOver?.Invoke(_currentPiece);
    }

    private void CreatePieceFromBag()
    {
        _currentPiece = _bag.NextPiece();

        if (!_tetrisField.CheckPositions(GetPositions()))
        {
            _currentPiece = null;
            OnSpawnBlocked?.Invoke();
            return;
        }

        OnCreateNewPiece?.Invoke();
    }

    public Piece GetCurrentPiece()
    {
        return _currentPiece;
    }

    private void PlacePiece()
    {
        if (_currentPiece == null) return;

        _tetrisField.Place(_currentPiece);

        _isCanHold = true;

        OnUpdateHold?.Invoke(_holdPiece, _isCanHold);

        CreatePieceFromBag();
    }

    private void PlayerHold()
    {
        if (!_isCanHold || _currentPiece == null) return;

        _isCanHold = false;

        Piece holdPiece = null;

        if (_holdPiece != null)
        {
            holdPiece = _holdPiece;
        }

        _currentPiece.ResetPiece();

        _holdPiece = _currentPiece;

        OnUpdateHold?.Invoke(_holdPiece, _isCanHold);

        if (holdPiece != null)
        {
            _currentPiece = holdPiece;
        }
        else
        {
            CreatePieceFromBag();
        }
    }

    public void Down()
    {
        _downTimer += Time.deltaTime;

        (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(Vector2Int.down, GetPositions(), _currentPiece.PivotPosition);

        bool isOnGround = !_tetrisField.CheckPositions(newMove.positions);

        //Debug.Log(_isAnyMove);

        if (isOnGround && _resetLockDelayCounter < MAX_RESET_LOCK_DELAY_COUNT && _isAnyMove)
        {
            //Фигура косается земли, но мы даем игроку сдвинуть её ещё пару раз

            _resetLockDelayCounter++;

            _downTimer = 0;
        }

        if (!isOnGround && _downTimer >= _gameParameters.TimeForDown)
        {
            //Фигура в воздухе, опускаем

            _resetLockDelayCounter = 0;

            _downTimer = 0;

            _currentPiece.PivotPosition = newMove.pivotPosition;

            FoundFinalPositions();

            return;
        }
        else if (isOnGround && _downTimer >= _gameParameters.LockDelay)
        {
            //Фигура на земле и игрок не двигает её или уже не может двигать

            _downTimer = 0;

            _resetLockDelayCounter = 0;

            _currentPiece.FinalPositons = GetPositions();

            PlacePiece();
        }

        FoundFinalPositions();

        _isAnyMove = false;

        return;
    }

    //Вспомогательный метод, который сдвигает пивот и позиции фигуры на вектор
    private (List<Vector2Int>, Vector2Int) Move(Vector2Int moveVector, List<Vector2Int> positions, Vector2Int pivotPositions)
    {
        List<Vector2Int> newPositions = new List<Vector2Int>();

        Vector2Int newPivotPosition = pivotPositions + moveVector;

        foreach (Vector2Int position in positions)
        {
            Vector2Int newPosition = new Vector2Int(position.x + moveVector.x, position.y + moveVector.y);

            newPositions.Add(newPosition);
        }


        return (newPositions, newPivotPosition);
    }

    private void TryRotate()
    {
        _currentPiece.Rotate();

        List<Vector2Int> positions = GetPositions();
        bool flag = _tetrisField.CheckPositions(positions);

        //Проверяем можем ли повернуть фигуру в данной позиции, если нет, то пытаемся сдвинуть ниже
        if (flag)
        {
            _isAnyMove = true;

            FoundFinalPositions();
            return;
        }


        List<Vector2Int> tryPos = new List<Vector2Int>();

        //Сдвигаем влево
        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.left);
        }

        flag = _tetrisField.CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;
            _currentPiece.PivotPosition += Vector2Int.left;

            FoundFinalPositions();
            return;
        }

        //Сдвигаем второй раз влево
        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.left);
        }

        flag = _tetrisField.CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;
            _currentPiece.PivotPosition += (Vector2Int.left + Vector2Int.left);

            FoundFinalPositions();
            return;
        }
        tryPos.Clear();


        //Сдвигаем вправо
        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.right);
        }

        flag = _tetrisField.CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;

            _currentPiece.PivotPosition += Vector2Int.right;

            FoundFinalPositions();
            return;
        }


        //Сдвигаем второй раз вправо
        foreach (var pos in positions)
        {
            tryPos.Add(pos + Vector2Int.right);
        }

        flag = _tetrisField.CheckPositions(tryPos);

        if (flag)
        {
            _isAnyMove = true;

            _currentPiece.PivotPosition += (Vector2Int.right + Vector2Int.right);

            FoundFinalPositions();
            return;
        }

        //Отменяем вращение, если не получилось найти положение для фигуры
        _currentPiece.UndoRotate();
    }

    private void FastDown()
    {
        (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(Vector2Int.down, GetPositions(), _currentPiece.PivotPosition);

        if (_tetrisField.CheckPositions(newMove.positions))
        {
            _currentPiece.PivotPosition = newMove.pivotPosition;

            _downTimer = 0f;

            FoundFinalPositions();
        }
    }

    private void HorizonalMove(Vector2Int direction)
    {
        (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(direction, GetPositions(), _currentPiece.PivotPosition);

        if (_tetrisField.CheckPositions(newMove.positions))
        {
            _currentPiece.PivotPosition = newMove.pivotPosition;

            _isAnyMove = true;

            FoundFinalPositions();
        }
    }

    public List<Vector2Int> GetPositions()
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        string[,] currentShape = _currentPiece.GetCurrenShape();

        for (int x = 0; x < currentShape.GetLength(1); x++)
        {
            for (int y = 0; y < currentShape.GetLength(0); y++)
            {
                if (currentShape[y, x].ToLower() == "x")
                    positions.Add(new Vector2Int(_currentPiece.PivotPosition.x + x, _currentPiece.PivotPosition.y + (3 - y)));
            }
        }

        return positions;
    }

    private void FoundFinalPositions()
    {
        _currentPiece.Position = GetPositions();


        List<Vector2Int> prevPositions = new();

        for (int i = 0; i <= _currentPiece.PivotPosition.y + 3; i++)
        {
            List<Vector2Int> positions = new();

            foreach (Vector2Int position in _currentPiece.Position)
            {
                positions.Add(new Vector2Int(position.x, position.y - i));
            }

            if (!_tetrisField.CheckPositions(positions) && prevPositions.Count > 0)
            {
                _currentPiece.FinalPositons.Clear();

                _currentPiece.FinalPositons.AddRange(prevPositions);

                return;
            }

            prevPositions = positions;
        }
    }
}
