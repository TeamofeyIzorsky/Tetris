using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TetrisFieldView : MonoBehaviour
{
    //[SerializeField] private GameManager gameManager; 

    [SerializeField] private Grid _grid;
    //[SerializeField] private GameObject _gameObject;

    //[SerializeField] private TetrisField testField;

    private Block[,] blocks;


    //[SerializeField] private Stack<Block> _blocksPool = new Stack<Block>();
    // [SerializeField] private Dictionary<Vector2Int, Block> _activeBlocks = new Dictionary<Vector2Int, Block>();



    //[SerializeField] private List

    private void Awake()
    {
        G.TetrisField.OnAfterUpdate += UpdateView;

        CreateField();
    }


    private void CreateField()
    {
        blocks = new Block[TetrisField.WIDTH, TetrisField.HEIGHT];

        for (int y = 0; y < TetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < TetrisField.WIDTH; x++)
            {
                GameObject blockGameObject = Instantiate(G.GResources.BlockPrefab, _grid.CellToWorld(new Vector3Int(x, y, 0)), _grid.transform.rotation, _grid.transform);

                Block block = blockGameObject.GetComponent<Block>();

                block.ChangeActive(false);

                blocks[x, y] = block;
            }
        }
    }

    private void UpdateView(IReadOnlyList<Vector2Int> piecePositions, Cell[,] grid, Piece currentPiece, IReadOnlyList<Vector2Int> finalPositions)
    {
        for (int y = 0; y < TetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < TetrisField.WIDTH; x++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (grid[x, y].Occupied)
                {
                    blocks[x, y].ChangeActive(true);


                    blocks[x, y].SetColor(grid[x, y].Color);
                }
                else if (piecePositions.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);


                    blocks[x, y].SetColor(currentPiece.color);
                }
                else if (finalPositions.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                    Color color = new Color(currentPiece.color.r, currentPiece.color.g, currentPiece.color.b, 0.075f);

                    blocks[x, y].SetColor(color);
                }
                else
                {
                    blocks[x, y].ChangeActive(false);
                }
            }
        }
    }
}
