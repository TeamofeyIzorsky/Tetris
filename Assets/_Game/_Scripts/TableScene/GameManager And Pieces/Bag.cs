using System;
using System.Collections.Generic;

public class Bag : IBag
{
    //Класс, отвечающий за хранение следующих фигур и их рандомизацию
    private ITetrisField _tetrisField;
    private IPlayerInput _playerInput;
    private IGameParameters _gameParameters;

    public Bag(ITetrisField field, IPlayerInput playerInput, IGameParameters gameParameters)
    {
        _tetrisField = field;
        _playerInput = playerInput;
        _gameParameters = gameParameters;
    }


    private List<Piece> _bag = new();

    public event Action<IReadOnlyList<Piece>> OnBagUpdate;

    //Отдаем новую следующую фигуру
    public Piece NextPiece()
    {
        if (_bag.Count < 7)
        {
            ExpandBag();
        }

        Piece nextPiece = _bag[0];

        _bag.Remove(nextPiece);

        OnBagUpdate?.Invoke(GetNextFivePieces());

        return nextPiece;
    }

    //Возвращаем лист с следующими фигурами
    private IReadOnlyList<Piece> GetNextFivePieces()
    {
        List<Piece> returnList = new List<Piece>();

        for (int x = 0; x < 5; x++)
        {
            returnList.Add(_bag[x]);
        }

        return returnList;
    }

    //Расширяем бэг, если он кончается
    private void ExpandBag()
    {
        Piece[] bag = new Piece[]
        {
            new OBlock(),
            new ZBlock(),
            new SBlock(),
            new LBlock(),
            new JBlock(),
            new IBlock(),
            new TBlock(),
        };

        //Тасование Фишера — Йетса

        for (int i = 0; i < bag.Length; i++)
        {
            Piece piece = bag[i];

            int randIndex = UnityEngine.Random.Range(0, 7);
            bag[i] = bag[randIndex];
            bag[randIndex] = piece;
        }

        foreach (Piece piece in bag)
        {
            _bag.Add(piece);
        }
    }

    public void InsertPiece(Piece piece, int index)
    {
        _bag.Insert(index, piece);
    }

    public IReadOnlyList<Piece> GetPieces()
    {
        return GetNextFivePieces();
    }
}
