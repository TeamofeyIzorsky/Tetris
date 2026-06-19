using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FieldView : MonoBehaviour
{
    //Компнент, который отображает игровое поле

    private GameObject _blockPrefab;
    private IPlayerInput _playerInput;

    public void Construct(IGameManager gameManager, IPlayerInput playerInput, ITicketManager ticketManager, GameResourcesSO gameResources)
    {
        _playerInput = playerInput;

        _blockPrefab = gameResources.BlockPrefab;

        gameManager.OnGameManagerTickOver += UpdateView;

        _theme = ticketManager.GetGameTicket().Theme;

        CreateField();

        UpdateView(null, gameManager.GetCurrentPiece());
    }


    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private Grid _grid;

    private ThemeSO _theme;

    private Block[,] blocks;


    private void CreateField()
    {
        blocks = new Block[TetrisField.WIDTH, TetrisField.HEIGHT];

        for (int y = 0; y < TetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < TetrisField.WIDTH; x++)
            {
                GameObject blockGameObject = Instantiate(_blockPrefab, _grid.CellToWorld(new Vector3Int(x, y, 0)), _grid.transform.rotation, _grid.transform);

                Block block = blockGameObject.GetComponent<Block>();

                block.ChangeActive(false);

                blocks[x, y] = block;
            }
        }
    }

    private void UpdateView(ITetrisField tetrisField, Piece currentPiece)
    {
        int[,] grid = null;

        if(tetrisField != null)
        {
            grid = tetrisField.GetGrid();
        }

        List<Vector2Int> posistions = currentPiece.GetPositions();

        Vector2Int? randomLastPlace = null;

        if(tetrisField != null)
        {
            randomLastPlace = tetrisField.GetRandomLastPlace();
        }

        for (int y = 0; y < TetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < TetrisField.WIDTH; x++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (_playerInput.HardDrop && randomLastPlace.HasValue && randomLastPlace.Value == position)
                {
                    _particleSystem.transform.position = blocks[x, y].transform.position + new Vector3(0.22f, 0.22f, 0);
                    _particleSystem.Play();
                }

                if (grid != null && grid[x, y] != 0)
                {

                    blocks[x, y].ChangeActive(true);

                    var theme = _theme.GetTheme(grid[x, y]);

                    blocks[x, y].SetColor(theme.Item1);
                    blocks[x, y].SetSprite(theme.Item2);
                }
                else if (posistions.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                    //Debug.Log(G.DataManager.currentGameData.Theme);
                    var theme = _theme.GetTheme(currentPiece.id);


                    blocks[x, y].SetColor(theme.Item1);
                    blocks[x, y].SetSprite(theme.Item2);
                }
                else if (currentPiece.FinalPositons.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                   // Debug.Log(G.DataManager.currentGameData.Theme);

                    var theme = _theme.GetTheme(currentPiece.id);

                    Color color = new Color(theme.Item1.r, theme.Item1.g, theme.Item1.b, 0.075f);

                    blocks[x, y].SetColor(color);
                    blocks[x, y].SetSprite(theme.Item2);
                }
                else
                {
                    blocks[x, y].ChangeActive(false);
                }
            }

        }
    }
}
