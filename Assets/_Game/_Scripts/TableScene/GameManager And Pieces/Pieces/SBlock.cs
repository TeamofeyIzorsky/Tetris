using UnityEngine;

public class SBlock : Piece
{
    public SBlock(IPlayerInput playerInput, ITetrisField tetrisField, IGameParameters gameParameters) : base(tetrisField, playerInput, gameParameters)
    {
        id = 6;

        string[,] shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", "X", " " },
            {"X", "X", " ", " " },
            {" ", " ", " ", " " }

        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", " ", " " },
            {" ", "X", "X", " " },
            {" ", " ", "X", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", " ", " ", " " },
            {" ", "X", "X", " " },
            {"X", "X", " ", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {"X", " ", " ", " " },
            {"X", "X", " ", " " },
            {" ", "X", " ", " " }
        };

        shapes.Add(shape);
    }
}
