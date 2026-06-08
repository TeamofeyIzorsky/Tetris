using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    //Отсюда мы передаем инпут игрока
    [HideInInspector] public bool Pause;

    [HideInInspector] public bool HardDown;
    [HideInInspector] public bool Rotate;
    [HideInInspector] public bool HoldPiece;

    [HideInInspector] public bool Down;
    [HideInInspector] public bool Left;
    [HideInInspector] public bool Right;

    [HideInInspector] public bool LeftHold;
    [HideInInspector] public bool RightHold;
    [HideInInspector] public bool DownHold;

    protected bool _isActive;

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    protected void Clear()
    {
        Pause = false;

        HardDown = false;
        Rotate = false;
        HoldPiece = false;

        Down = false;
        Left = false;
        Right = false;

        LeftHold = false;
        RightHold = false;
        DownHold = false;
    }
}
