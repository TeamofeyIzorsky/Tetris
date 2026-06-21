using UnityEngine;
using System.Collections.Generic;
using System;

public class TetrisField : ITetrisField
{
    //Класс отвечающий за логику игрового поля

    public TetrisField()
    {
        _grid = new int[ITetrisField.WIDTH, ITetrisField.HEIGHT];
    }

    //Grid Settings

    private int[,] _grid;

    private int _allDestroyedLines = 0;

    //private List<Vector2Int> _lastPlace;

    //Events
    public event Action<int, int> OnDeleteLinesEnd;
    public event Action<List<Vector2Int>> OnFieldUpdate;



    //Устанавливаем фигуру
    public void Place(Piece piece)
    {
        //Debug.Log("PLACE!");
        List<Vector2Int> lastPlace = piece.FinalPositons;

        foreach (var position in lastPlace)
        {
            _grid[position.x, position.y] = piece.id;
        }

        FoundAndDeleteFillLines();

        OnFieldUpdate?.Invoke(lastPlace);

    }

    //После установки удаляем полные линии и сдвигаем верхние линии вниз
    private void FoundAndDeleteFillLines()
    {
        int deletedLines = 0;


        for (int y = ITetrisField.HEIGHT - 1; y >= 0; y--)
        {
            bool isFilledLine = true;

            for (int x = 0; x < ITetrisField.WIDTH; x++)
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

                for (int x = 0; x < ITetrisField.WIDTH; x++)
                {
                    _grid[x, y] = 0;
                }

                for (int iy = y; iy < 22; iy++)
                {
                    for (int ix = 0; ix < ITetrisField.WIDTH; ix++)
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

    public int GetDeletedLinesCount()
    {
        return _allDestroyedLines;
    }

    public int GetBlockStatus(Vector2Int position)
    {
        return _grid[position.x, position.y];
    }
}

