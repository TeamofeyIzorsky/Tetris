using System.Collections.Generic;
using UnityEngine;

public class NextPieceView : MonoBehaviour
{
    [SerializeField] protected List<Block> _firstLine = new();
    [SerializeField] protected List<Block> _secondLine = new();

    float startY;
    float startX;

    private void Awake()
    {
        startY = transform.localPosition.y;
        startX = transform.localPosition.x;

        foreach(var block in _firstLine)
        {
            block.ChangeActive(false);
        }

        foreach (var block in _secondLine)
        {
            block.ChangeActive(false);
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
                _firstLine[x].ChangeActive(true);

                _firstLine[x].SetColor(piece.color);
                firstLine += 1;
            }
            else
            {
                _firstLine[x].ChangeActive(false);
            }

            if (shape[2, x].ToLower() == "x")
            {
                _secondLine[x].ChangeActive(true);

                _secondLine[x].SetColor(piece.color);
                secondLine += 1;
            }
            else
            {
                _secondLine[x].ChangeActive(false);
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
