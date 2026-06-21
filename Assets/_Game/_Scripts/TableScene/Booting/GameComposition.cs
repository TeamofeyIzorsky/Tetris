using UnityEngine;

public class GameComposition
{
    //Global
    public ITicketManager TicketManager {  get; private set; }
    public ILoadManager LoadManager { get; private set; }
    public IGameDataManager GameDataManager { get; private set; }
    public GameResourcesSO GameResources { get; private set; }


    //Local
    public IGameStateMachine GameStateMachine { get; private set; }
    public IUpdateManager UpdateManager { get; private set; }
    public IPlayerInput PlayerInput { get; private set; }

    public IPauseController PauseController { get; private set; }
    public ITetrisField TetrisField { get; private set; }
    public IBag Bag { get; private set; }
    public IGameInputHandler GameInputHandler { get; private set; }
    public IPieceController PieceController { get; private set; }
    public IGameScore GameScore { get; private set; }
    public IGameEndController GameEndController { get; private set; }
    public IGameParameters GameParameters { get; private set; }

    public GameComposition(ITicketManager ticketManager, ILoadManager loadManager, IGameDataManager gameDataManager, GameConfigSO gameConfig, GameResourcesSO gameResources)
    {
        TicketManager = ticketManager;
        LoadManager = loadManager;
        GameDataManager = gameDataManager;
        GameResources = gameResources;

        GameStateMachine = new GameStateMachine();


        UpdateManager = new GameObject("Update Manager Object").AddComponent<UpdateManager>().Construct(GameStateMachine);

        PlayerInput = new OldInputSystem();

        PauseController = new PauseController(PlayerInput, GameStateMachine);

        TetrisField = new TetrisField();

        GameParameters = new GameParameters(gameConfig, TetrisField);

        Bag = new Bag(TetrisField, PlayerInput, GameParameters);

        GameInputHandler = new GameInputHandler(PlayerInput, GameParameters);

        PieceController = new PieceController(GameInputHandler, TetrisField, GameParameters, Bag);

        GameScore = new GameScore(TicketManager, TetrisField);


        IEndStrategy endStrategy = TicketManager.GetGameTicket().GameMode switch
        {
            GameMode.Blitz => new BlitzEndStrategy(),
            GameMode.Lines40 => new Lines40EndStategy(),
            GameMode.Standard => new StandardEndStategy(),
            _ => new StandardEndStategy()
        };

        GameEndController = new GameEndController(endStrategy, GameScore, PieceController, GameStateMachine, gameDataManager);
    }

    public void CreateUpdateOrder()
    {
        UpdateManager.Add(PlayerInput);
        UpdateManager.Add(PauseController);
        UpdateManager.Add(GameInputHandler);
        UpdateManager.Add(PieceController);
        UpdateManager.Add(GameScore);
    }

}
