using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetPosition(Vector3 positon)
    {
        transform.position = positon;
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void ChangeActive(bool activeStatus)
    {
        _spriteRenderer.enabled = activeStatus;
    }
}
