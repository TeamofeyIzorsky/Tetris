using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PausableBehaviour, IGameManager
{
    //Класс отвечающий за управление основным игровым процессом

    //Зависимости
    private IBag _bag;
    private ITetrisField _tetrisField;


    private int _currentSpeedLevel = 0;

    private Piece _currentPiece;

    private Piece _holdPiece;
    private bool _isCanHold = true;


    public event Action<IReadOnlyList<Piece>> OnUpdateBag;
    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action OnGameOver;
    public event Action<ITetrisField, Piece> OnTickOver;


    public void Init(IBag bag, ITetrisField tetrisField)
    {
        _bag = bag;
        _tetrisField = tetrisField;
    }


    private void Start()
    {
        CreatePieceFromBag();

        OnTickOver?.Invoke(_tetrisField, _currentPiece);
    }

    protected override void PausableUpdate()
    {
        Down();

        if (G.PlayerInput.HardDown)
        {

            PlacePiece();

            OnTickOver?.Invoke(_tetrisField, _currentPiece);
            return;
        }
        else if (G.PlayerInput.HoldPiece)
        {
            //Холдим фигуру в карман
            PlayerHold();

            OnTickOver?.Invoke(_tetrisField, _currentPiece);
            return;
        }

        if (G.PlayerInput.Rotate)
        {
            //Поворот фигуры
            PlayerRotatePiece();
        }

        if (G.PlayerInput.Down || G.PlayerInput.DownHold)
        {
            //Ускоренный спуск вниз

            PlayerFastDown();
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

        OnTickOver?.Invoke(_tetrisField, _currentPiece);
    }

    //Создаем новую фигуру на поле из бэга
    private void CreatePieceFromBag()
    {
        _currentPiece = _bag.NextPiece();

        if (_currentPiece.CheckCreate())
        {
            Debug.Log("GAMEOVER!");

            OnGameOver?.Invoke();

            return;
        }
    }

    private void PlacePiece()
    {
        _tetrisField.Place(_currentPiece);

        CreatePieceFromBag();

        _isCanHold = true;

        CheckSpeedLevel();

        OnUpdateHoldPiece?.Invoke(_holdPiece, _isCanHold);
    }

    //Игрок холдит фигуру
    private void PlayerHold()
    {
        if (!_isCanHold) return;

        _isCanHold = false;

        if (_holdPiece != null)
        {
            _bag.InsertPiece(_holdPiece, 0);
        }

        _currentPiece.ResetPiece();

        _holdPiece = _currentPiece;

        OnUpdateHoldPiece?.Invoke(_holdPiece, _isCanHold);

        CreatePieceFromBag();
    }

    private void PlayerRotatePiece()
    {
        _currentPiece.TryRotate();
    }

    private void Down()
    {
        if (_currentPiece.Down())
        {
            PlacePiece();
        }
    }

    //Игрок двигает фигуру вниз быстрее
    private void PlayerFastDown()
    {
        if (_currentPiece.FastDown())
        {
            PlacePiece();
        }
    }

    //Игрок двигает фигуру горизонтально
    private void PlayerMoveHorizontal(Vector2Int direction)
    {
        if (_currentPiece.HorizontalMove(direction))
        {
            PlacePiece();
        }
    }


    //Проверяем не должны ли мы ускорить игру
    private void CheckSpeedLevel()
    {
        SpeedLevel speedLevel = G.GResources.SpeedLevels[_currentSpeedLevel];

        if (_tetrisField.GetDeletedLinesCount() >= speedLevel.LinesCount)
        {
            if (_currentSpeedLevel < G.GResources.SpeedLevels.Count - 1)
            {
                _currentSpeedLevel++;
            }

            G.GameConfig.NewDownSpeed(speedLevel.Speed);
        }
    }
}
