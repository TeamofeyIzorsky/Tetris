using UnityEngine;

public class OldInputSystem : PlayerInput
{
    protected void Update()
    {
        if (!_isActive)
        {
            Clear();

            return;
        }

        Pause = Input.GetKeyDown(KeyCode.Escape);

        HardDown = Input.GetKeyDown(KeyCode.Space);
        Rotate = Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.UpArrow);
        HoldPiece = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.C);

        Left = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        Right = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        Down = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        LeftHold = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        RightHold = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        DownHold = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }
}
