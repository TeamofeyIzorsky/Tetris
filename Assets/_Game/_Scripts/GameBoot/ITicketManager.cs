using UnityEngine;

public interface ITicketManager
{
    public GameTicket GetGameTicket();
    public void SetGameTicket(GameTicket gameTicket);
}
