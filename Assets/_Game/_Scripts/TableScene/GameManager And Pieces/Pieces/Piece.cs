using System.Collections.Generic;
using UnityEngine;

public abstract class Piece
{
    //Класс, отвечающий за поведение фигуры на поле

    private ITetrisField _tetrisField;
    private IPlayerInput _playerInput;
    private IGameParameters _gameParameters;

    public Piece(ITetrisField tetrisField, IPlayerInput playerInput, IGameParameters gameParameters)
    {
        _tetrisField = tetrisField;
        _playerInput = playerInput;
        _gameParameters = gameParameters;
    }

    //Точка спавна на поле
    const int DEFUALT_SPAWN_X_POSITION = 3;
    const int DEFUALT_SPAWN_Y_POSITION = 19;


    public Vector2Int PivotPosition { get; private set; } = new Vector2Int(DEFUALT_SPAWN_X_POSITION, DEFUALT_SPAWN_Y_POSITION);
    public List<Vector2Int> FinalPositons { get; private set; } = new List<Vector2Int>();

    public List<string[,]> shapes = new List<string[,]>();

    private int _currentRotate = 0;


    //Спуск вниз
    private float _downTimer = 0;
    private float _downMoveTimer = 0;

    //Движение в стороны
    private float _horizontalTimer = 0;
    private int _horizontalMovesCount = 0;

    private Vector2Int _prevDirection = new Vector2Int();


    //Задержка установки фигуры при касании земли
    private const int MAX_RESET_LOCK_DELAY_COUNT = 15;

    private int _resetLockDelayCounter;
    private bool _isAnyMove;

    public int id {  get; protected set; }


    //Проверка не занято ли место спавна
    public bool IsSpawnPositionValid()
    {
        if (!_tetrisField.CheckPositions(GetPositions()))
        {
            Debug.Log("GAMEOVER!");

            return true;
        }

        return false;
    }

    //Стирает состояние фигуры
    public void ResetPiece()
    {
        _downTimer = 0;
        _horizontalMovesCount = 0;
        _horizontalTimer = 0;

        _currentRotate = 0;
        PivotPosition = new Vector2Int(DEFUALT_SPAWN_X_POSITION, DEFUALT_SPAWN_Y_POSITION);
        FinalPositons = new List<Vector2Int>();
    }

    //Рассчитывает и возвращает позицию всех клеток, занятых фигурой
    public List<Vector2Int> GetPositions()
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int x = 0; x < shapes[_currentRotate].GetLength(1); x++)
        {
            for (int y = 0; y < shapes[_currentRotate].GetLength(0); y++)
            {
                if (shapes[_currentRotate][y, x].ToLower() == "x")
                    positions.Add(new Vector2Int(PivotPosition.x + x, PivotPosition.y + (3 - y)));
            }
        }

        return positions;
    }

    //Считает самую низкую позицию, которую может занять фигура в данной позиции
    private void FoundFinalPositions()
    {
        List<Vector2Int> prevPositions = new();

        for (int i = 0; i <= PivotPosition.y + 3; i++)
        {
            List<Vector2Int> positions = new();

            foreach (Vector2Int position in GetPositions())
            {
                positions.Add(new Vector2Int(position.x, position.y - i));
            }

            if (!_tetrisField.CheckPositions(positions) && prevPositions.Count > 0)
            {
                //Debug.Log("FOUND!");

                FinalPositons.Clear();

                FinalPositons.AddRange(prevPositions);

                return;
            }

            prevPositions = positions;
        }
    }

    //Метод, отвечающий за постепенное опускание фигуры
    public bool Down()
    {
        _downTimer += Time.deltaTime;

        (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(Vector2Int.down, GetPositions(), PivotPosition);

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

            PivotPosition = newMove.pivotPosition;

            FoundFinalPositions();

            return false;
        }
        else if (isOnGround && _downTimer >= _gameParameters.LockDelay)
        {
            //Фигура на земле и игрок не двигает её или уже не может двигать

            _downTimer = 0;

            _resetLockDelayCounter = 0;

            FinalPositons = GetPositions();

            return true;
        }

        FoundFinalPositions();

        _isAnyMove = false;

        return false;
    }

    //Двигаем фигуру горизонтально
    public bool HorizontalMove(Vector2Int direction)
    {
        if ((_playerInput.Left || _playerInput.Right) || direction != _prevDirection)
        {
            //Мгновенно сдвигаем при начале движения

            _horizontalMovesCount = 1;

            _horizontalTimer = 0;

            _prevDirection = direction;


            (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(direction, GetPositions(), PivotPosition);

            if (_tetrisField.CheckPositions(newMove.positions))
            {
                PivotPosition = newMove.pivotPosition;

                _isAnyMove = true;

                FoundFinalPositions();
            }

            //Debug.Log("Slow!");
        }
        else if ((_playerInput.LeftHold || _playerInput.RightHold) && _prevDirection == direction)
        {
            //Сдвигаем с задержкой, если кнопка зажата

            _horizontalTimer += Time.deltaTime;

            if (_horizontalMovesCount >= 2)
            {

                if (_horizontalTimer >= _gameParameters.TimeForFastHorizontalMove)
                {
                    //Ускоренно двигаем горизонатально

                    _horizontalTimer = 0;

                    _horizontalMovesCount++;

                    (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(direction, GetPositions(), PivotPosition);

                    if (_tetrisField.CheckPositions(newMove.positions))
                    {
                        PivotPosition = newMove.pivotPosition;

                        _isAnyMove = true;

                        FoundFinalPositions();
                    }

                    _prevDirection = direction;
                }
            }
            else
            {

                if (_horizontalTimer >= _gameParameters.TimeForStartHorizontalMove)
                {
                    //Первые движения горизонтально делаем медленее

                    _horizontalTimer = 0;

                    _horizontalMovesCount++;

                    (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(direction, GetPositions(), PivotPosition);

                    if (_tetrisField.CheckPositions(newMove.positions))
                    {
                        PivotPosition = newMove.pivotPosition;

                        _isAnyMove = true;

                        FoundFinalPositions();
                    }

                    _prevDirection = direction;
                }
            }
        }

        FoundFinalPositions();

        return false;
    }

    //Ускоренный спуск ниже игроком
    public bool FastDown()
    {
        if (_playerInput.Down)
        {
            //Мгновенно спускаем, если это начало движения вниз или единичное нажатие

            _downTimer = 0;

            (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(Vector2Int.down, GetPositions(), PivotPosition);

            if (_tetrisField.CheckPositions(newMove.positions))
            {
                PivotPosition = newMove.pivotPosition;

                FoundFinalPositions();
            }
        }
        else if (_playerInput.DownHold)
        {
            //Спускаем с задержкой, если кнопка зажата

            _downTimer = 0;

            _downMoveTimer += Time.deltaTime;

            if (_downMoveTimer >= _gameParameters.TimeForFastDown)
            {
                _downMoveTimer = 0;

                (List<Vector2Int> positions, Vector2Int pivotPosition) newMove = Move(Vector2Int.down, GetPositions(), PivotPosition);

                if (_tetrisField.CheckPositions(newMove.positions))
                {
                    PivotPosition = newMove.pivotPosition;

                    FoundFinalPositions();
                }
            }
        }

        return false;

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

    //Пытаемся повернуть
    public void TryRotate()
    {
        Rotate();

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
            PivotPosition += Vector2Int.left;

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
            PivotPosition += (Vector2Int.left + Vector2Int.left);

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

            PivotPosition += Vector2Int.right;

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

            PivotPosition += (Vector2Int.right + Vector2Int.right);

            FoundFinalPositions();
            return;
        }

        //Отменяем вращение, если не получилось найти положение для фигуры
        UndoRotate();
    }

    private void Rotate()
    {
        _currentRotate++;

        if (_currentRotate == shapes.Count)
        {
            _currentRotate = 0;
        }
    }

    private void UndoRotate()
    {
        _currentRotate--;

        if (_currentRotate == -1)
        {
            _currentRotate = shapes.Count - 1;
        }
    }
}
