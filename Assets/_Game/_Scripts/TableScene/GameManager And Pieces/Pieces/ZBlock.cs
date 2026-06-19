using UnityEngine;

public class ZBlock : Piece
{
    public ZBlock(IPlayerInput playerInput, ITetrisField tetrisField, IGameParameters gameParameters) : base(tetrisField, playerInput, gameParameters)
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
