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

        var theme = G.DataManager.currentGameData.Theme.GetTheme(piece);

        base.ShowPiece(piece);

        foreach(var block in _firstLine)
        {
            block.SetSprite(theme.Item2);

            if (active)
            {
                block.SetColor(theme.Item1);
            }
            else
            {
                block.SetColor(new Color32(125, 125, 125, 255));
            }
        }

        foreach (var block in _secondLine)
        {
            block.SetSprite(theme.Item2);

            if (active)
            {
                block.SetColor(theme.Item1);
            }
            else
            {
                block.SetColor(new Color32(125, 125, 125, 255));
            }
        }
    }
}
