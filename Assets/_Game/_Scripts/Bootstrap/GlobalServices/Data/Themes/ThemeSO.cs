using UnityEngine;

public class ThemeSO : ScriptableObject
{
    //РАБОТА В ПРОЦЕССЕ, хранит в себе информацию о спрайтах и цветах разных фигур, чтобы можно было менять тему игры

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



    public (Color, Sprite) GetTheme(int piece)
    {
        Sprite sprite = null;
        Color color = Color.white;

        switch(piece)
        {

            default:
                return (color, sprite);

            case 1: //T
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
                break;

            case 2: //I
                if (ThemeSpiteType == ThemeSpiteType.SingleSpite)
                {
                    sprite = SingleSprite;
                }
                else
                {
                    sprite = IPieceSprite;
                }

                if (ThemeColorType == ThemeColorType.DefualSpritesColor)
                {
                    color = Color.white;
                }
                else
                {
                    color = IPieceColor;
                }
                break;
                 //L
            case 3:
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
                break;

            case 4: //J
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
                break;

            case 5://Z
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
                break;

            case 6: //S
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
                break;

            case 7: //O
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
                break;
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
