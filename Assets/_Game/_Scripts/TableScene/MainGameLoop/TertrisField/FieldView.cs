using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FieldView : MonoBehaviour
{
    //Компнент, который отображает игровое поле

    private GameObject _blockPrefab;
    private IPlayerInput _playerInput;
    private ITetrisField _tetrisField;

    public void Construct(IPieceController pieceController, ITetrisField tetrisField, IPlayerInput playerInput, ITicketManager ticketManager, GameResourcesSO gameResources)
    {
        _tetrisField = tetrisField;

        _playerInput = playerInput;

        _blockPrefab = gameResources.BlockPrefab;

        pieceController.PieceControllerTickOver += UpdateCurrentPieceView;

        _tetrisField.OnFieldUpdate += UpdateFieldView;


        CreateField(ticketManager.GetGameTicket().Theme);

        UpdateCurrentPieceView(pieceController.GetCurrentPiece());
    }


    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private Grid _grid;

    private Block[,] _blocks;

    private List<Vector2Int> _lastCurrentPiecePositions = new List<Vector2Int>();


    private void CreateField(ThemeSO theme)
    {
        _blocks = new Block[ITetrisField.WIDTH, ITetrisField.HEIGHT];

        for (int y = 0; y < ITetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < ITetrisField.WIDTH; x++)
            {
                GameObject blockGameObject = Instantiate(_blockPrefab, _grid.CellToWorld(new Vector3Int(x, y, 0)), _grid.transform.rotation, _grid.transform);

                Block block = blockGameObject.GetComponent<Block>();

                block.Construct(theme);

                block.UpdateBlockView(0);

                _blocks[x, y] = block;
            }
        }
    }


    private void UpdateFieldView(List<Vector2Int> lastPlace)
    {
        for (int y = 0; y < ITetrisField.HEIGHT; y++)
        {
            for (int x = 0; x < ITetrisField.WIDTH; x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                
                int blockStatus = _tetrisField.GetBlockStatus(position);

                if (blockStatus == 0 && _lastCurrentPiecePositions.Contains(position))
                {
                    continue;
                }

                _blocks[x, y].UpdateBlockView(blockStatus);

                if (lastPlace != null && lastPlace.Contains(position))
                {
                    _blocks[position.x, position.y].PlayGlow();

                    if (_playerInput.HardDrop)
                    {
                        _particleSystem.transform.position = _blocks[x, y].transform.position + new Vector3(0.22f, 0.22f, 0);
                        _particleSystem.Play();
                    }
                }
            }
        }
    }

    private void UpdateCurrentPieceView(Piece currentPiece)
    {
        foreach (var lastPosition in _lastCurrentPiecePositions)
        {
            if (_tetrisField.GetBlockStatus(lastPosition) == 0)
            {
                _blocks[lastPosition.x, lastPosition.y].UpdateBlockView(0);
            }
        }

        _lastCurrentPiecePositions.Clear();


        List<Vector2Int> positions;
        List<Vector2Int> ghostPositions;

        if (currentPiece != null)
        {
            positions = currentPiece.Position;
            ghostPositions = currentPiece.FinalPositons;

            foreach (var position in ghostPositions)
            {
                if (_tetrisField.GetBlockStatus(position) == 0)
                {
                    _blocks[position.x, position.y].UpdateBlockView(currentPiece.id, true);
                }
            }

            foreach (var position in positions)
            {
                if (_tetrisField.GetBlockStatus(position) == 0)
                {
                    _blocks[position.x, position.y].UpdateBlockView(currentPiece.id);
                }
            }

            _lastCurrentPiecePositions.AddRange(positions);
            _lastCurrentPiecePositions.AddRange(ghostPositions);
            return;
        }

        _lastCurrentPiecePositions.Clear();
    }
}
