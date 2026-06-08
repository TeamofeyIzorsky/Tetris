using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private bool _selectedOn = false;
    [SerializeField, Range(0.05f, 10f)] private float _selectedDuration = 1f;
    public MainButtonStateSetting SelectSetting = new();

    [SerializeField] private bool _clickOn = true;
    [SerializeField, Range(0.05f, 10f)] private float _clickDuration = 1f;
    public MainButtonStateSetting ClickSetting = new();

    private MainButtonDefualtSetting _defualtSettings = new();

    private Image _buttonImage;
    private TMP_Text _text;
    private Sequence _currentAnimation;

    private bool _inside;
    private bool _click;

    
    
    /*    public void OnPointerClick(PointerEventData eventData)
        {
            if (!_clickOn)
            {
                return;
            }

            if (_currentAnimation != null)
            {
                _currentAnimation.Kill();
            }

            _currentAnimation = DOTween.Sequence();

            if (ClickSetting.ChangeButtonSize)
            {
                _currentAnimation.Append(transform.DOScale(ClickSetting.SizeButtonMultiply, _clickDuration));
            }

            if (ClickSetting.ChangeButtonColor)
            {
                _currentAnimation.Join(_buttonImage.DOColor(ClickSetting.SelectedButtonColor, _clickDuration));
            }

            if (ClickSetting.ChangeTextSize)
            {
                _currentAnimation.Join(_text.transform.DOScale(ClickSetting.SizeTextMultiply, _clickDuration));
            }

            if (ClickSetting.ChangeTextColor)
            {
                _currentAnimation.Join(_text.DOColor(ClickSetting.SelectedTextColor, _clickDuration));
            }
        }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_clickOn)
        {
            return;
        }

        if (_currentAnimation != null)
        {
            _currentAnimation.Kill();
        }

        _currentAnimation = DOTween.Sequence();

        _click = true;


        if (ClickSetting.ChangeButtonSize)
        {
            _currentAnimation.Append(transform.DOScale(ClickSetting.SizeButtonMultiply, _selectedDuration));
        }
        else
        {
            _currentAnimation.Append(transform.DOScale(1f, _selectedDuration));
        }

        if (ClickSetting.ChangeButtonColor)
        {
            _currentAnimation.Join(_buttonImage.DOColor(ClickSetting.SelectedButtonColor, _selectedDuration));
        }
        else
        {
            _currentAnimation.Join(_buttonImage.DOColor(_defualtSettings.DefualtButtonColor, _selectedDuration));
        }

        if (ClickSetting.ChangeTextSize)
        {
            _currentAnimation.Join(_text.transform.DOScale(ClickSetting.SizeTextMultiply, _selectedDuration));
        }
        else
        {
            _currentAnimation.Join(_text.transform.DOScale(1f, _selectedDuration));
        }

        if (ClickSetting.ChangeTextColor)
        {
            _currentAnimation.Join(_text.DOColor(ClickSetting.SelectedTextColor, _selectedDuration));
        }
        else
        {
            _currentAnimation.Join(_text.DOColor(_defualtSettings.DefualtTextColor, _selectedDuration));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _click = false;

        if (_inside)
        {
            OnPointerEnter(null);
        }
        else
        {
            OnPointerExit(null);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inside = true;

        if (!_selectedOn || _click)
        {
            return;
        }

        if (_currentAnimation != null)
        {
            _currentAnimation.Kill();
        }

        _currentAnimation = DOTween.Sequence();

        if (SelectSetting.ChangeButtonSize)
        {
            _currentAnimation.Append(transform.DOScale(SelectSetting.SizeButtonMultiply, _selectedDuration));
        }
        else
        {
            _currentAnimation.Append(transform.DOScale(1f, _selectedDuration));
        }

        if (SelectSetting.ChangeButtonColor)
        {
            _currentAnimation.Join(_buttonImage.DOColor(SelectSetting.SelectedButtonColor, _selectedDuration));
        }
        else
        {
            _currentAnimation.Join(_buttonImage.DOColor(_defualtSettings.DefualtButtonColor, _selectedDuration));
        }

        if (SelectSetting.ChangeTextSize)
        {
            _currentAnimation.Join(_text.transform.DOScale(SelectSetting.SizeTextMultiply, _selectedDuration));
        }
        else
        {
            _currentAnimation.Join(_text.transform.DOScale(1f, _selectedDuration));
        }

        if (SelectSetting.ChangeTextColor)
        {
            _currentAnimation.Join(_text.DOColor(SelectSetting.SelectedTextColor, _selectedDuration));
        }
        else
        {
            _currentAnimation.Join(_text.DOColor(_defualtSettings.DefualtTextColor, _selectedDuration));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inside = false;

        if (!_selectedOn || _click)
        {
            return;
        }

        if (_currentAnimation != null)
        {
            _currentAnimation.Kill();
        }

        _currentAnimation = DOTween.Sequence();


        _currentAnimation.Append(transform.DOScale(1f, _selectedDuration));

        _currentAnimation.Join(_buttonImage.DOColor(_defualtSettings.DefualtButtonColor, _selectedDuration));

        _currentAnimation.Join(_text.transform.DOScale(1f, _selectedDuration));

        _currentAnimation.Join(_text.DOColor(_defualtSettings.DefualtTextColor, _selectedDuration));
    }

    private void Start()
    {
        _buttonImage = GetComponent<Image>();

        _defualtSettings.DefualtButtonColor = _buttonImage.color;

        _text = GetComponentInChildren<TMP_Text>();

        _defualtSettings.DefualtTextColor = _text.color;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;

        if(_buttonImage != null ) _buttonImage.color = _defualtSettings.DefualtButtonColor;

        if (_text != null) _text.transform.localScale = Vector3.one;

        if (_text != null) _text.color = _defualtSettings.DefualtTextColor;
    }
}

[System.Serializable]
public class MainButtonStateSetting
{
    [Header("Button Size")]
    public bool ChangeButtonSize = true;
    [Range(0.1f, 5)]
    public float SizeButtonMultiply = 1.25f;

    [Header("Button Color")]
    public bool ChangeButtonColor = false;
    public Color SelectedButtonColor = Color.white;

    [Header("Text Size")]
    public bool ChangeTextSize = false;
    [Range(0.1f, 5)]
    public float SizeTextMultiply = 1.25f;


    [Header("Text Color")]
    public bool ChangeTextColor = false;
    public Color SelectedTextColor = Color.white;
}

public class MainButtonDefualtSetting
{
    [HideInInspector]
    public Color DefualtButtonColor;

    [HideInInspector]
    public Color DefualtTextColor;
}
