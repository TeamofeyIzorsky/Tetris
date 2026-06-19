using UnityEngine;

public class OBlock : Piece
{
    public OBlock(IPlayerInput playerInput, ITetrisField tetrisField, IGameParameters gameParameters) : base(tetrisField, playerInput, gameParameters) 
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
