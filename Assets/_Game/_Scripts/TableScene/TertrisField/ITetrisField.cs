using System;
using System.Collections.Generic;
using UnityEngine;

public interface ITetrisField
{
    public const int HEIGHT = 24;
    public const int WIDTH = 10;

    public event Action<int, int> OnDeleteLinesEnd;

    public event Action<List<Vector2Int>> OnFieldUpdate;


    //public int[,] GetGrid();
    public int GetBlockStatus(Vector2Int position);
    public void Place(Piece piece);
    public int GetDeletedLinesCount();
    public bool CheckPositions(List<Vector2Int> positions);
}
