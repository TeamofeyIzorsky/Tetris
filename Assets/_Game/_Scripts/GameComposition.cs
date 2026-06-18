using UnityEngine;

public class GameComposition
{
    public IUpdateManager UpdateManager { get; private set; }
    public IPlayerInput PlayerInput { get; private set; }
    public IPauseController PauseController { get; private set; }

    public ITetrisField TetrisField { get; private set; }

    public IBag Bag { get; private set; }
    public IGameManager GameManager { get; private set; }
    public IGameScore GameScore { get; private set; }
    public IEndGameManager EndGameManager { get; private set; }


    public GameComposition()
    {
        UpdateManager = new GameObject("Update Manager Object").AddComponent<UpdateManager>();

        PlayerInput = new OldInputSystem();

        PauseController = new PauseController(PlayerInput, UpdateManager);

        G.GameConfig = new GameConfig(G.GResources);
        TetrisField = new TetrisField(G.GameConfig.Theme);

        Bag = new Bag(TetrisField, PlayerInput);

        GameManager = new GameManager(Bag, TetrisField, PlayerInput);

        GameMode gameMode = GameMode.Standart;

        GameScore = new GameScore(gameMode, TetrisField, GameManager);

        EndGameManager = new GameEndManager(gameMode, GameScore, PlayerInput, PauseController);
    }

    public void CreateUpdateOrder()
    {
        UpdateManager.Add(PlayerInput);
        UpdateManager.Add(PauseController);
        UpdateManager.Add(GameManager);
        UpdateManager.Add(GameScore);
    }

}
