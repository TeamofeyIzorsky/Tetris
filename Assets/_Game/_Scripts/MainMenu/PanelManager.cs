using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private List<Panel> _panels = new List<Panel>();
    [SerializeField, Range(0.05f, 10)] float _animationDuration = 1.0f;

    private Panel _currenPanel = null;

    public void OpenPanel(string panelName)
    {
        if (_currenPanel != null && _currenPanel.Name == panelName) return;

        foreach(Panel panel in _panels)
        {
            if (panel.Name == panelName)
            {
                _currenPanel = panel;

                _currenPanel.CanvasGroup.gameObject.SetActive(true);

                Sequence animation = DOTween.Sequence();

                animation.Append(_currenPanel.CanvasGroup.DOFade(1f, _animationDuration).From(0f).Play()); 
                animation.Join(_currenPanel.CanvasGroup.gameObject.transform.DOScale(1f, _animationDuration).From(0.75f).Play());

                continue;
            }

            panel.CanvasGroup.gameObject.SetActive(false);
        }
    }   

    public void ClosePanels()
    {
        _currenPanel = null;

        foreach(Panel panel in _panels)
        {
            panel.CanvasGroup.gameObject.SetActive(false);
        }
    }


}

[System.Serializable]
public class Panel
{
    public string Name;
    public CanvasGroup CanvasGroup;
}
