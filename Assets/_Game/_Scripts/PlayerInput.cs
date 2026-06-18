using UnityEngine;

public interface IPlayerInput : IUpdatable
{
    //Отсюда мы передаем инпут игрока
    public bool Pause { get; }

    public bool HardDrop { get; }
    public bool Rotate { get; }
    public bool HoldPiece { get; }

    public bool Down { get; }
    public bool Left { get; }
    public bool Right { get; }

    public bool LeftHold { get; }
    public bool RightHold { get; }
    public bool DownHold { get; }

}
