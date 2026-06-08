using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour
{
    [SerializeField] private List<NextPieceView> _nextPieceViews = new();

    private void Awake()
    {
        G.TetrisField.OnUpdateBag += UpdateBag;
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
