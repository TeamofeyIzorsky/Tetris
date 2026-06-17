using UnityEngine;

public class OldInputSystem : MonoBehaviour, IPlayerInput
{
    private bool Pause;
    private bool HardDrop;
    private bool Rotate;

    private bool HoldPiece;

    private bool Left;
    private bool Right;

    private bool LeftHold;
    private bool RightHold;

    private bool Down;
    private bool DownHold;

    bool IPlayerInput.Pause { get => Pause; }
    bool IPlayerInput.HardDrop { get => HardDrop; }
    bool IPlayerInput.Rotate { get => Rotate; }
    bool IPlayerInput.HoldPiece { get => HoldPiece; }
    bool IPlayerInput.Left { get => Left; }
    bool IPlayerInput.Right { get => Right; }
    bool IPlayerInput.LeftHold { get => LeftHold; }
    bool IPlayerInput.RightHold { get => RightHold;}
    bool IPlayerInput.Down { get => Down; }
    bool IPlayerInput.DownHold { get => DownHold; }

    private bool _isActive;

    protected void Update()
    {
        if (!_isActive)
        {
            Clear();

            return;
        }

        Pause = Input.GetKeyDown(KeyCode.Escape);

        HardDrop = Input.GetKeyDown(KeyCode.Space);
        Rotate = Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.UpArrow);
        HoldPiece = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.C);

        Left = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        Right = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        Down = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        LeftHold = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        RightHold = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        DownHold = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    public void SetActive(bool isActive)
    {
        throw new System.NotImplementedException();
    }

    protected void Clear()
    {
        Pause = false;

        HardDrop = false;
        Rotate = false;
        HoldPiece = false;

        Left = false;
        Right = false;
        Down = false;

        LeftHold = false;
        RightHold = false;

        DownHold = false;
    }
}
