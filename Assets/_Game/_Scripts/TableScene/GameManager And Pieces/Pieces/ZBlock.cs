using UnityEngine;

public class ZBlock : Piece
{
    public ZBlock()
    {
        id = 5;

        string[,] shape = new string[,]
        {
            {" ", " ", " ", " " },
            {"X", "X", " ", " " },
            {" ", "X", "X", " " },
            {" ", " ", " ", " " }

        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", " ", "X", " " },
            {" ", "X", "X", " " },
            {" ", "X", " ", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", " ", " ", " " },
            {"X", "X", " ", " " },
            {" ", "X", "X", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", " ", " " },
            {"X", "X", " ", " " },
            {"X", " ", " ", " " }
        };

        shapes.Add(shape);
    }
}
