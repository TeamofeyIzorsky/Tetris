using UnityEngine;

public interface IEndStrategy
{
    //Определяет условия для конца игры в зависимости от режима

    public void Subscribe(IGameEndController gameEndController, IGameScore gameScore, IPieceController pieceController);
}
