using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour
{
    //Отображение следующих фигур

    [SerializeField] private List<NextPieceView> _nextPieceViews = new();

    public  void Construct(IBag bag)
    {
        bag.OnBagUpdate += UpdateBag;
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
