using UnityEngine;

public class HoldPieceView : NextPieceView
{
    private void Start()
    {
        G.TetrisField.OnUpdateHoldPiece += ShowHoldPiece;
    }

    public void ShowHoldPiece(Piece piece, bool active)
    {
        if(piece == null) return;

        base.ShowPiece(piece);

        foreach(var block in _firstLine)
        {
            if (active)
            {
                block.SetColor(piece.color);
            }
            else
            {
                block.SetColor(new Color32(125, 125, 125, 255));
            }
        }

        foreach (var block in _secondLine)
        {
            if (active)
            {
                block.SetColor(piece.color);
            }
            else
            {
                block.SetColor(new Color32(125, 125, 125, 255));
            }
        }
    }
}
