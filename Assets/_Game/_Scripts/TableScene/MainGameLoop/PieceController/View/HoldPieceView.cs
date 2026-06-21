using UnityEngine;

public class HoldPieceView : NextPieceView
{
    //Компнент, который отображает удержанную фигуру
    public void Construct(IPieceController pieceController, ITicketManager ticketManager)
    {
        base.Construct(ticketManager.GetGameTicket().Theme);

        pieceController.OnUpdateHold += ShowHoldPiece;
    }


    public void ShowHoldPiece(Piece piece, bool active)
    {
        if(piece == null) return;

        base.ShowPiece(piece);

        if (!active)
        {
            foreach (var block in _firstLine)
            {
                if (!block.isEnabled)
                {
                    block.UpdateBlockView(0);
                    continue;
                }

                block.UpdateBlockView(piece.id, true);
            }

            foreach (var block in _secondLine)
            {
                if (!block.isEnabled)
                {
                    block.UpdateBlockView(0);
                    continue;
                }

                block.UpdateBlockView(piece.id, true);
            }
        }
    }
}
