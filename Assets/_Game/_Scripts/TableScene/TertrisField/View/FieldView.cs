using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TetrisFieldView : MonoBehaviour
{
    //Компнент, который отображает игровое поле

    [SerializeField] private Grid _grid;

    private Block[,] blocks;

    private void Start()
    {
        G.GameManager.OnTickOver += UpdateView;

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

    private void UpdateView(ITetrisField tetrisField, Piece currentPiece)
    {
        Cell[,] grid = tetrisField.GetGrid();
        List<Vector2Int> posistions = currentPiece.GetPositions();

        for (int y = 0; y < TetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < TetrisField.WIDTH; x++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (grid[x, y].Occupied)
                {
                    blocks[x, y].ChangeActive(true);


                    blocks[x, y].SetColor(grid[x, y].Color);
                    blocks[x, y].SetSprite(grid[x, y].Sprite);
                }
                else if (posistions.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                    //Debug.Log(G.DataManager.currentGameData.Theme);
                    var theme = G.GameConfig.Theme.GetTheme(currentPiece);


                    blocks[x, y].SetColor(theme.Item1);
                    blocks[x, y].SetSprite(theme.Item2);
                }
                else if (currentPiece.FinalPositons.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                   // Debug.Log(G.DataManager.currentGameData.Theme);

                    var theme = G.GameConfig.Theme.GetTheme(currentPiece);

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

    /*private void UpdateView(IReadOnlyList<Vector2Int> piecePositions, Cell[,] grid, Piece currentPiece, IReadOnlyList<Vector2Int> finalPositions)
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
                    blocks[x, y].SetSprite(grid[x, y].Sprite);
                }
                else if (piecePositions.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                    Debug.Log(G.DataManager.currentGameData.Theme);
                    var theme = G.DataManager.currentGameData.Theme.GetTheme(currentPiece);


                    blocks[x, y].SetColor(theme.Item1);
                    blocks[x, y].SetSprite(theme.Item2);
                }
                else if (finalPositions.Contains(position))
                {
                    blocks[x, y].ChangeActive(true);

                    Debug.Log(G.DataManager.currentGameData.Theme);

                    var theme = G.DataManager.currentGameData.Theme.GetTheme(currentPiece);

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
    }*/
}
