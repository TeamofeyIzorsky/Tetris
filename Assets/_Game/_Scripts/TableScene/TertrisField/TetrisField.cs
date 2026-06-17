using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class TetrisField : ITetrisField
{
    //Класс отвечающий за логику игрового поля

    //Grid Settings
    public const int HEIGHT = 24;
    public const int WIDTH = 10;

    private Cell[,] _grid;

    private int _destroyedLines = 0;

    private Vector2Int? _randomLastPlace;

    //Events
    public event Action<int, int> OnDeleteLinesEnd;
    
    public void Init()
    {
        _grid = new Cell[WIDTH, HEIGHT];
    }

    //Устанавливаем фигуру
    public void Place(Piece piece)
    {
        //Debug.Log("PLACE!");

        foreach (var position in piece.FinalPositons)
        {
            _grid[position.x, position.y].Occupied = true;

            var theme = G.GameConfig.Theme.GetTheme(piece);

            _grid[position.x, position.y].Color = theme.Item1;

            _grid[position.x, position.y].Sprite = theme.Item2;
        }

        var randomPiece = piece.FinalPositons[Random.Range(0, piece.FinalPositons.Count)];

        _randomLastPlace = new Vector2Int(randomPiece.x, randomPiece.y);

        FoundAndDeleteFillLines();
    }

    //После установки удаляем полные линии и сдвигаем верхние линии вниз
    private void FoundAndDeleteFillLines()
    {
        int deleteCount = 0;


        for (int y = HEIGHT - 1; y >= 0; y--)
        {
            bool flagLine = true;

            for (int x = 0; x < WIDTH; x++)
            {
                if (!_grid[x, y].Occupied)
                {
                    flagLine = false;
                }
            }

            if (flagLine)
            {
                _destroyedLines++;
                deleteCount++;

                for (int x = 0; x < WIDTH; x++)
                {
                    _grid[x, y].Occupied = false;
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
        if(deleteCount > 0)
        {
            OnDeleteLinesEnd?.Invoke(deleteCount, _destroyedLines);
        }
    }
   
    public bool CheckPositions(List<Vector2Int> positions)
    {
        bool isCorrectPositions = true;

        foreach (Vector2Int position in positions)
        {
            if (position.y < 0 || position.x > 9 || position.x < 0 || _grid[position.x, position.y].Occupied)
            {
                isCorrectPositions = false;
                break;
            }
        }

        return isCorrectPositions;
    }

    public Cell[,] GetGrid()
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
        return _destroyedLines;
    }
}

