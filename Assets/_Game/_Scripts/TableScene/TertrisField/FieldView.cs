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

                block.Construct(_theme);

                block.UpdateBlockView(0);

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

        List<Vector2Int> ghostPositions = currentPiece.FinalPositons;

        List<Vector2Int> lastPlace = null;

        if(tetrisField != null)
        {
            lastPlace = tetrisField.GetLastPlace();
        }

        for (int y = 0; y < TetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < TetrisField.WIDTH; x++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (lastPlace != null && lastPlace.Contains(position))
                {
                    blocks[position.x, position.y].PlayGlow();

                    if (_playerInput.HardDrop)
                    {
                        _particleSystem.transform.position = blocks[x, y].transform.position + new Vector3(0.22f, 0.22f, 0);
                        _particleSystem.Play();
                    }
                }

                if (posistions != null && posistions.Contains(position))
                {
                    blocks[x, y].UpdateBlockView(currentPiece.id);
                }
                else if (ghostPositions != null && ghostPositions.Contains(position))
                {
                    blocks[x, y].UpdateBlockView(currentPiece.id, true);
                }
                else if (grid != null)
                {
                    blocks[x, y].UpdateBlockView(grid[x, y]);

                    //blocks[x, y].PlayGlow();
                }
            }
        }
    }
}
