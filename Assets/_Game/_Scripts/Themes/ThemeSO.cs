using UnityEngine;

public class ThemeSO : ScriptableObject
{
    [Header("Theme Settings")]
    public ThemeSpiteType ThemeSpiteType;
    public ThemeColorType ThemeColorType;

    [Header("Single-Sprite Theme")]
    public Sprite SingleSprite;


    [Header("TPiece Settings")]
    public Sprite TPieceSprite;
    public Color TPieceColor;

    [Header("IPiece Settings")]
    public Sprite IPieceSprite;
    public Color IPieceColor;

    [Header("LPiece Settings")]
    public Sprite LPieceSprite;
    public Color LPieceColor;

    [Header("JPiece Settings")]
    public Sprite JPieceSprite;
    public Color JPieceColor;

    [Header("ZPiece Settings")]
    public Sprite ZPieceSprite;
    public Color ZPieceColor;

    [Header("SPiece Settings")]
    public Sprite SPieceSprite;
    public Color SPieceColor;

    [Header("OPiece Settings")]
    public Sprite OPieceSprite;
    public Color OPieceColor;



    public (Color, Sprite) GetTheme(Piece piece)
    {
        Sprite sprite = null;
        Color color = Color.white;

        if (piece is TBlock)
        {
            if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = TPieceSprite;
            }

            if (ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = TPieceColor;
            }

        }
        else if (piece is IBlock)
        {
            if(ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = IPieceSprite;
            }

            if(ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = IPieceColor;
            }

        }
        else if (piece is LBlock)
        {
            if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = LPieceSprite;
            }

            if (ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = LPieceColor;
            }

        }
        else if (piece is JBlock)
        {
            if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = JPieceSprite;
            }

            if (ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = JPieceColor;
            }

        }
        else if (piece is ZBlock)
        {
            if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = ZPieceSprite;
            }

            if (ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = ZPieceColor;
            }

        }
        else if (piece is SBlock)
        {
            if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = SPieceSprite;
            }

            if (ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = SPieceColor;
            }

        }
        else if (piece is OBlock)
        {
            if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
            {
                sprite = SingleSprite;
            }
            else
            {
                sprite = OPieceSprite;
            }

            if (ThemeColorType == ThemeColorType.DefualSpritesColor)
            {
                color = Color.white;
            }
            else
            {
                color = OPieceColor;
            }

        }

        return (color, sprite);
    }

}

public enum ThemeColorType
{
    DefualSpritesColor,
    SevenColors,
}
public enum ThemeSpiteType
{
    SingleSpite,
    SevenSprites,
}
