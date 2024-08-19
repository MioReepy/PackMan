using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer { get; private set; }
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _animationTime;
    public int AnimationFrame { get; private set; }
    private bool _loop = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), _animationTime, _animationTime);
    }

    private void Advance()
    {
        if (!this._spriteRenderer.enabled)
        {
            return;
        }

        AnimationFrame++;

        if (AnimationFrame >= _sprites.Length && _loop)
        {
            AnimationFrame = 0;
        }

        if (AnimationFrame >= 0 && AnimationFrame < _sprites.Length)
        {
            this._spriteRenderer.sprite = _sprites[AnimationFrame];
        }
    }

    private void Restart()
    {
        AnimationFrame = -1;
        Advance();
    }
}
