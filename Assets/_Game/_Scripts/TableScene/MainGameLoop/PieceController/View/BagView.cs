using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour
{
    //Отображение следующих фигур

    [SerializeField] private List<NextPieceView> _nextPieceViews = new();

    public void Construct(IBag bag, ITicketManager ticketManager)
    {
        bag.OnBagUpdate += UpdateBag;

        foreach (var pieceView in _nextPieceViews)
        {
            pieceView.Construct(ticketManager.GetGameTicket().Theme);
        }

        UpdateBag(bag.GetPieces());

    }

    private void UpdateBag(IReadOnlyList<Piece> bag)
    {
        for(int x = 0; x < _nextPieceViews.Count; x++)
        {
            //Debug.Log("UpdateBag!");

            _nextPieceViews[x].ShowPiece(bag[x]);
        }
    }
}
