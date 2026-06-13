using UnityEngine;

public class IBlock : Piece
{
    public IBlock()
    {
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
