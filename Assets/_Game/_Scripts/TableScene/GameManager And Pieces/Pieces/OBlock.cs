using UnityEngine;

public class OBlock : Piece
{
    public OBlock(IPlayerInput playerInput, ITetrisField tetrisField, IGameParameters gameParameters) : base(tetrisField, playerInput, gameParameters) 
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
