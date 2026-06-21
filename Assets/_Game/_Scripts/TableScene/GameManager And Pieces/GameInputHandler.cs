using System;
using UnityEngine;

public class GameInputHandler : IGameInputHandler
{

    private IPlayerInput _playerInput;

    private IGameParameters _gameParameters;

    public bool IsPausable { get => true; set => throw new NotImplementedException(); }

    public GameInputHandler(IPlayerInput playerInput, IGameParameters gameParameters)
    {
        _playerInput = playerInput;

        _gameParameters = gameParameters;
    }

    private float _fastDownTimer = 0;


    private float _horizontalMoveTimer = 0;
    private int _horizontalMovesCount = 1;
    private Vector2Int _prevHorizontalDirection = new Vector2Int();


    public event Action OnHardDrop;
    public event Action OnHold;
    public event Action OnRotate;
    public event Action OnFastDown;
    public event Action<Vector2Int> OnHorizonralMove;

    public void Tick(float deltaTime)
    {
        InputProcess();
    }

    private void InputProcess()
    {
        if (_playerInput.HardDrop)
        {
            OnHardDrop?.Invoke();

            return;
        }
        else if (_playerInput.HoldPiece)
        {

            OnHold?.Invoke();
            return;
        }

        if (_playerInput.Rotate)
        {
            OnRotate?.Invoke();
        }

        if (_playerInput.Down || _playerInput.DownHold)
        {
            //Ускоренный спуск вниз

            FastDownProcess();
        }

        if (_playerInput.Left || _playerInput.LeftHold)
        {
            //Влыво

            HorizontalMoveHandler(Vector2Int.left);
        }

        else if (_playerInput.Right || _playerInput.RightHold)
        {
            //Вправо

            HorizontalMoveHandler(Vector2Int.right);
        }

    }

    private void FastDownProcess()
    {
        if (_playerInput.Down)
        {
            OnFastDown?.Invoke();
            return;
        }

        if (_playerInput.DownHold)
        {
            _fastDownTimer += Time.deltaTime;

            if (_fastDownTimer >= _gameParameters.TimeForFastDown)
            {
                _fastDownTimer = 0;

                OnFastDown?.Invoke();
            }
        }
    }

    private void HorizontalMoveHandler(Vector2Int direction)
    {
        if ((_playerInput.Left || _playerInput.Right) || direction != _prevHorizontalDirection)
        {
            _horizontalMovesCount = 0;

            _horizontalMoveTimer = 0;

            _prevHorizontalDirection = direction;

            OnHorizonralMove?.Invoke(direction);
            return;
        }

        if ((_playerInput.LeftHold || _playerInput.RightHold) && _prevHorizontalDirection == direction)
        {
            _horizontalMoveTimer += Time.deltaTime;

            _prevHorizontalDirection = direction;

            if (_horizontalMovesCount >= 2)
            {

                if (_horizontalMoveTimer >= _gameParameters.TimeForFastHorizontalMove)
                {
                    _horizontalMoveTimer = 0;

                    _horizontalMovesCount++;

                    OnHorizonralMove?.Invoke(direction);

                    return;
                }
            }
            else
            {
                if (_horizontalMoveTimer >= _gameParameters.TimeForStartHorizontalMove)
                {
                    _horizontalMoveTimer = 0;

                    _horizontalMovesCount++;

                    OnHorizonralMove?.Invoke(direction);
                }
            }
        }
    }
}
