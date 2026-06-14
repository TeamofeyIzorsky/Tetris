using System;
using System.Collections.Generic;
using UnityEngine;

public interface ITetrisField
{
    public event Action<int, int> OnDeleteLinesEnd;


    public Cell[,] GetGrid();
    public void Place(Piece piece);
    public int GetDeletedLinesCount();
    public bool CheckPositions(List<Vector2Int> positions);
}
