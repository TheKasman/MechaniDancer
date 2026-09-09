using Godot;

public partial class BlueSlime : CharacterBody2D
{
    [Export]
    public int TileSize = 105;

    private bool _movedUp = false;

    public override void _Ready()
    {
        TrueSoul.Instance.BeatHit += OnBeatHit;
    }

    private void OnBeatHit(int beatIndex)
    {
        if (beatIndex % 2 == 0)
        {
            return;
        }
        else
        {
            if (_movedUp)
            {
                Position += Vector2.Down * TileSize;
                _movedUp = false;
            }
            else
            {
                Position = Vector2.Up * TileSize;
                _movedUp = true;
            }
        }
    }
}
