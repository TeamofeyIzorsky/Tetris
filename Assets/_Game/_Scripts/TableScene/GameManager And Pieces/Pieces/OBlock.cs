using UnityEngine;

public class OBlock : Piece
{
    public OBlock(IPlayerInput playerInput, ITetrisField tetrisField) : base(tetrisField, playerInput)
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
