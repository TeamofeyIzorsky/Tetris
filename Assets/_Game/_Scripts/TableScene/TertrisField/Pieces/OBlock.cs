using UnityEngine;

public class OBlock : Piece
{
    public OBlock()
    {
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
