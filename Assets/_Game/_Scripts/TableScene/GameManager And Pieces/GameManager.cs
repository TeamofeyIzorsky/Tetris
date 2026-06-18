using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IGameManager
{
    //Класс отвечающий за управление основным игровым процессом

    //Зависимости
    private IBag _bag;
    private ITetrisField _tetrisField;
    private IPlayerInput _playerInput;
    private IGameParameters _gameParametes;

    public GameManager(IBag bag, ITetrisField tetrisField, IPlayerInput playerInput)
    {
        _bag = bag;
        _tetrisField = tetrisField;
        _playerInput = playerInput;

        CreatePieceFromBag();
    }

    private Piece _currentPiece;

    private Piece _holdPiece;
    private bool _isCanHold = true;

    public bool IsPausable { get => true; set => throw new NotImplementedException(); }

    public event Action<Piece, bool> OnUpdateHoldPiece;
    public event Action OnGameOver;
    public event Action<ITetrisField, Piece> OnGameManagerTickOver;

    public void Tick(float deltaTime)
    {
        Down();
        
        ProcessInput();

        OnGameManagerTickOver?.Invoke(_tetrisField, _currentPiece);
    }

    private void ProcessInput()
    {
        if (_playerInput.HardDrop)
        {
            PlacePiece();

            return;
        }
        else if (_playerInput.HoldPiece)
        {
            //Холдим фигуру в карман
            PlayerHold();

            return;
        }

        if (_playerInput.Rotate)
        {
            //Поворот фигуры
            PlayerRotatePiece();
        }

        if (_playerInput.Down || _playerInput.DownHold)
        {
            //Ускоренный спуск вниз

            PlayerFastDown();
        }

        if (_playerInput.Left || _playerInput.LeftHold)
        {
            //Влыво

            PlayerMoveHorizontal(Vector2Int.left);
        }

        else if (_playerInput.Right || _playerInput.RightHold)
        {
            //Вправо

            PlayerMoveHorizontal(Vector2Int.right);
        }
    }

    //Создаем новую фигуру на поле из бэга
    private void CreatePieceFromBag()
    {
        _currentPiece = _bag.NextPiece();

        if (_currentPiece.IsSpawnPositionValid())
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
        _currentPiece.FastDown();
    }

    //Игрок двигает фигуру горизонтально
    private void PlayerMoveHorizontal(Vector2Int direction)
    {
        if (_currentPiece.HorizontalMove(direction))
        {
            PlacePiece();
        }
    }

    public Piece GetCurrentPiece()
    {
        return _currentPiece;
    }
}
