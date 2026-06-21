using UnityEngine;

public class TicketManager : ITicketManager
{
    //Переносит сообщения из одной сцены в другуюы

    private GameTicket _gameTicket;

    public GameTicket GetGameTicket()
    {
        return _gameTicket;
    }

    public void SetGameTicket(GameTicket gameTicket)
    {
        _gameTicket = gameTicket;
    }
}
