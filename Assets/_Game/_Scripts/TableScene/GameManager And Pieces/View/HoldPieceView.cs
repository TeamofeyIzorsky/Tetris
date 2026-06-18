using UnityEngine;

public class HoldPieceView : NextPieceView
{
    //Компнент, который отображает удержанную фигуру
    public void Construct(IGameManager gameManager)
    {
        gameManager.OnUpdateHoldPiece += ShowHoldPiece;
    }


    public void ShowHoldPiece(Piece piece, bool active)
    {
        if(piece == null) return;

        var theme = G.GameConfig.Theme.GetTheme(piece);

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
