using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Компонент, который отображает отдельную клетку игрового поля

    [SerializeField] private SpriteRenderer _spriteRenderer;
    public bool isEnabled => _spriteRenderer.enabled;


    //Анимация блика при установке блока
    [SerializeField] private Material _waveEffectMaterial;
    private MaterialPropertyBlock _propertyBlock;
    private static readonly int ProgressID = Shader.PropertyToID("_Progress");
    private Tween _tween;

    private int _lastPieceNum;

    private ThemeSO _themeSO;
    public void Construct(ThemeSO theme)
    {
        _themeSO = theme;

        _spriteRenderer.sharedMaterial = _waveEffectMaterial;

        _propertyBlock = new MaterialPropertyBlock();
    }

    public void PlayGlow()
    {
        // Останавливаем прошлую анимацию
        _tween?.Kill();

        // Устанавливаем начальное значение
        _propertyBlock.SetFloat(ProgressID, 0f);
        _spriteRenderer.SetPropertyBlock(_propertyBlock);

        // Анимируем через DOVirtual
        _tween = DOVirtual.Float(0f, 1f, 0.5f, (value) => 
        {
                _propertyBlock.SetFloat(ProgressID, value);
                _spriteRenderer.SetPropertyBlock(_propertyBlock);
        }
        ).SetEase(Ease.OutQuad);
    }

    public void UpdateBlockView(int pieceNum, bool isGhost = false)
    {
        if (pieceNum == 0)
        {
            _spriteRenderer.enabled = false;
            return;
        }

        if (_lastPieceNum != pieceNum)
        {
            _tween?.Kill();
        }

        _lastPieceNum = pieceNum;

        _spriteRenderer.enabled = true;

        var theme = _themeSO.GetTheme(pieceNum);

        if (isGhost)
        {
            _spriteRenderer.color = new Color(theme.Item1.r, theme.Item1.g, theme.Item1.b, 0.075f);
        }
        else
        {
            _spriteRenderer.color = theme.Item1;
        }

        _spriteRenderer.sprite = theme.Item2;
    }
}
