using UnityEngine;

public class OBlock : Piece
{
    public OBlock()
    {
        color = new Color32(220, 210, 65, 255);

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
