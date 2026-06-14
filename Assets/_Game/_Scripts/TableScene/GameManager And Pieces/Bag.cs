using System;
using System.Collections.Generic;
using UnityEngine;

public class Bag : IBag
{
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
}
