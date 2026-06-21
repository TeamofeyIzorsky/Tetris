using System.Collections.Generic;
using UnityEngine;

public abstract class Piece
{
    //Точка спавна на поле
    const int DEFUALT_SPAWN_X_POSITION = 3;
    const int DEFUALT_SPAWN_Y_POSITION = 19;

    public Vector2Int PivotPosition = new Vector2Int(DEFUALT_SPAWN_X_POSITION, DEFUALT_SPAWN_Y_POSITION);
    public List<Vector2Int> Position = new();
    public List<Vector2Int> FinalPositons = new List<Vector2Int>();

    public List<string[,]> shapes = new List<string[,]>();

    private int _currentRotate = 0;


    public int id {  get; protected set; }

    public string[,] GetCurrenShape()
    {
        return shapes[_currentRotate];
    }

    //Стирает состояние фигуры
    public void ResetPiece()
    {
        _currentRotate = 0;
        PivotPosition = new Vector2Int(DEFUALT_SPAWN_X_POSITION, DEFUALT_SPAWN_Y_POSITION);
        FinalPositons = new List<Vector2Int>();
    }

    public void Rotate()
    {
        _currentRotate++;

        if (_currentRotate == shapes.Count)
        {
            _currentRotate = 0;
        }
    }

    public void UndoRotate()
    {
        _currentRotate--;

        if (_currentRotate == -1)
        {
            _currentRotate = shapes.Count - 1;
        }
    }
}
