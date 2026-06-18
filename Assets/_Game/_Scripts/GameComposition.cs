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

    public IGameParameters GameParameters { get; private set; }


    public GameComposition(IGameConfig gameConfig, GameMode gameMode, ThemeSO theme)
    {
        UpdateManager = new GameObject("Update Manager Object").AddComponent<UpdateManager>();

        PlayerInput = new OldInputSystem();

        PauseController = new PauseController(PlayerInput, UpdateManager);

        TetrisField = new TetrisField(theme);

        GameParameters = new GameParameters(gameConfig, TetrisField);

        Bag = new Bag(TetrisField, PlayerInput, GameParameters);

        GameManager = new GameManager(Bag, TetrisField, PlayerInput);

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
