using Godot;

public partial class BeatIndicator : ColorRect
{
    [Export]
    public float PulseScale = 1.5f;
    public float PulseSpeed = 8.0f;

    private Vector2 _baseScale;
    private Vector2 _targetScale;

    public override void _Ready()
    {
        _baseScale = Scale;
        _targetScale = _baseScale;
        PivotOffset = Size / 2;

        // LIKE COMMENT AND SUBSCRIBE to TrueSoul's signal
        TrueSoul.Instance.BeatHit += OnBeatHit;
    }

    private void OnBeatHit(int beatIndex)
    {
        // As soon as the beat fires... this should snap up to size
        GD.Print("BeatIndicator received: " + beatIndex);
        Scale = _baseScale * PulseScale;
    }

    public override void _Process(double delta)
    {
        // Ease back down every frame until it's back to it's base size
        Scale = Scale.Lerp(_baseScale, (float)delta * PulseSpeed);
    }
}
