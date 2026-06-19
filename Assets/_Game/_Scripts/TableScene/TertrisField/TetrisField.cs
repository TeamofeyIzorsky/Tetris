using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class TetrisField : ITetrisField
{
    //Класс отвечающий за логику игрового поля

    public TetrisField()
    {
        _grid = new int[WIDTH, HEIGHT];
    }

    //Grid Settings
    public const int HEIGHT = 24;
    public const int WIDTH = 10;

    private int[,] _grid;

    private int _allDestroyedLines = 0;

    private Vector2Int? _randomLastPlace;

    //Events
    public event Action<int, int> OnDeleteLinesEnd;
    


    //Устанавливаем фигуру
    public void Place(Piece piece)
    {
        //Debug.Log("PLACE!");

        foreach (var position in piece.FinalPositons)
        {
            _grid[position.x, position.y] = piece.id;
        }

        var randomPiece = piece.FinalPositons[Random.Range(0, piece.FinalPositons.Count)];

        _randomLastPlace = new Vector2Int(randomPiece.x, randomPiece.y);

        FoundAndDeleteFillLines();
    }

    //После установки удаляем полные линии и сдвигаем верхние линии вниз
    private void FoundAndDeleteFillLines()
    {
        int deletedLines = 0;


        for (int y = HEIGHT - 1; y >= 0; y--)
        {
            bool isFilledLine = true;

            for (int x = 0; x < WIDTH; x++)
            {
                if (_grid[x, y] == 0)
                {
                    isFilledLine = false;
                }
            }

            if (isFilledLine)
            {
                _allDestroyedLines++;
                deletedLines++;

                for (int x = 0; x < WIDTH; x++)
                {
                    _grid[x, y] = 0;
                }

                for (int iy = y; iy < 22; iy++)
                {
                    for (int ix = 0; ix < WIDTH; ix++)
                    {
                        _grid[ix, iy] = _grid[ix, iy + 1];

                    }
                }
            }
        }
        if(deletedLines > 0)
        {
            OnDeleteLinesEnd?.Invoke(deletedLines, _allDestroyedLines);
        }
    }
   
    public bool CheckPositions(List<Vector2Int> positions)
    {
        bool isCorrectPositions = true;

        foreach (Vector2Int position in positions)
        {
            if (position.y < 0 || position.x > 9 || position.x < 0 || _grid[position.x, position.y] != 0)
            {
                isCorrectPositions = false;
                break;
            }
        }

        return isCorrectPositions;
    }

    public int[,] GetGrid()
    {
        return _grid;
    }

    public Vector2Int? GetRandomLastPlace()
    {
        Vector2Int? random = _randomLastPlace;
        _randomLastPlace = null;

        return random;
    }

    public int GetDeletedLinesCount()
    {
        return _allDestroyedLines;
    }
}

