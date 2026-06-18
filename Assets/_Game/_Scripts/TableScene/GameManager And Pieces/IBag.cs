using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBag
{
    public event Action<IReadOnlyList<Piece>> OnBagUpdate;

    public void InsertPiece(Piece piece, int num);

    public IReadOnlyList<Piece> GetPieces();

    public Piece NextPiece();
}
