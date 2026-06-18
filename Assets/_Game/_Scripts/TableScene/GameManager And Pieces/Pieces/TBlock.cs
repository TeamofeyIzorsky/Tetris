using UnityEngine;

public class TBlock : Piece
{
    public TBlock(IPlayerInput playerInput, ITetrisField tetrisField, IGameParameters gameParameters) : base(tetrisField, playerInput, gameParameters)
    { 
        string[,] shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", " ", " " },
            {"X", "X", "X", " " },
            {" ", " ", " ", " " }

        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", " ", " " },
            {" ", "X", "X", " " },
            {" ", "X", " ", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", " ", " ", " " },
            {"X", "X", "X", " " },
            {" ", "X", " ", " " }
        };

        shapes.Add(shape);

        shape = new string[,]
        {
            {" ", " ", " ", " " },
            {" ", "X", " ", " " },
            {"X", "X", " ", " " },
            {" ", "X", " ", " " }
        };

        shapes.Add(shape);
    }
}
