using UnityEngine;

public class MenuBoot : MonoBehaviour
{
    //Класс, который запускает иницилизацию систем и инъекцию зависимостей для View на сцене

    [SerializeField] private DataView _dataView;
    [SerializeField] private StartNewGameTrigger _startGameTrigger;
    //[SerializeField] private 

    private MenuComposition _menuComposition;

    private void Awake()
    {
        _menuComposition = new MenuComposition(GlobalServices.GameDataManager, GlobalServices.LoadManager, GlobalServices.TicketManager, GlobalServices.Resources);

        _dataView.Construct(_menuComposition.GameDataManager);
        _startGameTrigger.Construct(_menuComposition.LoadManager, _menuComposition.TicketManager, _menuComposition.GameResourcesSO);
    }
}
