using UnityEngine;
using System.Collections.Generic;

public abstract class Piece
{
    public Color color = Color.white;

    public int currentRotate = 0;
    public List<string[,]> shapes = new List<string[,]>();

    public List<Vector2Int> GetPositions(Vector2Int pivotPosition)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int x = 0; x < shapes[currentRotate].GetLength(1); x++)
        {
            for (int y = 0; y < shapes[currentRotate].GetLength(0); y++)
            {
                if (shapes[currentRotate][y, x].ToLower() == "x")
                    positions.Add(new Vector2Int(pivotPosition.x + x, pivotPosition.y + (3 - y)));
            }
        }

        return positions;
    }

    public void Rotate()
    {
        currentRotate++;

        if (currentRotate == shapes.Count)
        {
            currentRotate = 0;
        }
    }

    public void UndoRotate()
    {
        currentRotate--;

        if (currentRotate == -1)
        {
            currentRotate = shapes.Count;
        }
    }
}
