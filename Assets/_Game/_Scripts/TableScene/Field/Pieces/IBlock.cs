using UnityEngine;

public class IBlock : Piece
{
    public IBlock()
    {
        color = new Color32(65, 220, 165, 255);

        string[,] shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", " ", " ", " " },
            {"X", "X", "X", "X" },
            {" ", " ", " ", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", "X", " ", " " },
            {" ", "X", " ", " " },
            {" ", "X", " ", " " },
            {" ", "X", " ", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {"X", "X", "X", "X" },
            {" ", " ", " ", " " },
            {" ", " ", " ", " " }

        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", "X", " " },
            {" ", " ", "X", " " },
            {" ", " ", "X", " " },
            {" ", " ", "X", " " }
        };

        shapes.Add(shape);
    }
}
