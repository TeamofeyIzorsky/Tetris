using UnityEngine;

public class StartNewGameTrigger : MonoBehaviour
{
    public void StartNewGame(string mode)
    {
        switch (mode)
        {
            case "Blitz":
                GlobalServices.TicketManager.SetGameTicket(new GameTicket(GameMode.Blitz, GlobalServices.Resources.ThemeSOs[0]));
                break;
            case "Standart":
                GlobalServices.TicketManager.SetGameTicket(new GameTicket(GameMode.Standard, GlobalServices.Resources.ThemeSOs[0]));
                break;
            case "40Lines":
                GlobalServices.TicketManager.SetGameTicket(new GameTicket(GameMode.Lines40, GlobalServices.Resources.ThemeSOs[0]));
                break;
            default:
                //NONE
                return;

        }

        GlobalServices.LoadManager.Load(new LoadSettings()
        {
            NeedFade = true,
            SceneName = "TableScene"
        });
    }
}
