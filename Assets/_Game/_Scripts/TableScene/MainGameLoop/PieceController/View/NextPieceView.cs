using System.Collections.Generic;
using UnityEngine;

public class NextPieceView : MonoBehaviour
{
    //Компонент, который отображает следующую фигуру

    protected ThemeSO _theme;

    [SerializeField] protected List<Block> _firstLine = new();
    [SerializeField] protected List<Block> _secondLine = new();

    float startY;
    float startX;

    public void Construct(ThemeSO theme)
    {
        _theme = theme;

        startY = transform.localPosition.y;
        startX = transform.localPosition.x;

        foreach (var block in _firstLine)
        {
            block.Construct(theme);

            block.UpdateBlockView(0);
        }

        foreach (var block in _secondLine)
        {
            block.Construct(theme);

            block.UpdateBlockView(0);
        }
    }

    public void ShowPiece(Piece piece)
    {
        if(piece == null || piece.shapes.Count == 0) return;

        //Debug.Log("NOT NULL");

        string[,] shape = piece.shapes[0];

        int firstLine = 0;
        int secondLine = 0;

        for (int x = 0; x < 4; x++)
        {
            if(shape[1, x].ToLower() == "x")
            {
                _firstLine[x].UpdateBlockView(piece.id);

                firstLine += 1;
            }
            else
            {
                _firstLine[x].UpdateBlockView(0);
            }

            if (shape[2, x].ToLower() == "x")
            {
                _secondLine[x].UpdateBlockView(piece.id);

                secondLine += 1;
            }
            else
            {
                _secondLine[x].UpdateBlockView(0);
            }
        }

        float xPos = 0;
        float yPos = 0;

        if ((secondLine != 4 && firstLine != 4) && (shape[2, 0] != shape[1, 3] || shape[1, 0] != shape[2, 3]))
        {
            xPos = startX + 0.6f;
        }
        else
        {
            xPos = startX;
        }

        if (secondLine == 4)
        {
            yPos = startY + 0.7f;
        }
        else if (firstLine == 4)
        {
            yPos = startY - 0.7f;
        }
        else
        {
            yPos = startY;
        }


        transform.localPosition = new Vector3(xPos, yPos, 0);
    }
}
