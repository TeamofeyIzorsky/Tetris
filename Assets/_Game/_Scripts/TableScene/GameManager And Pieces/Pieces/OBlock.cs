using UnityEngine;

public class OBlock : Piece
{
    public OBlock()
    {
        id = 7;

        string[,] shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", "X", " " },
            {" ", "X", "X", " " },
            {" ", " ", " ", " " }

        };

        shapes.Add(shape);
    }
}
