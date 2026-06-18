using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour
{
    //Отображение следующих фигур

    [SerializeField] private List<NextPieceView> _nextPieceViews = new();

    public void Construct(IBag bag, ThemeSO theme)
    {
        bag.OnBagUpdate += UpdateBag;

        foreach (var pieceView in _nextPieceViews)
        {
            pieceView.Construct(theme);
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
