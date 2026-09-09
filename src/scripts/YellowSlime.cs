using Godot;

public partial class YellowSlime : CharacterBody2D
{
    [Export]
    public int TileSize = 105;

    private int _moveState = 1;

    public override void _Ready()
    {
        TrueSoul.Instance.BeatHit += OnBeatHit;
    }

    private void OnBeatHit(int beatIndex)
    {
        switch (_moveState)
        {
            case 1:
            {
                Position += Vector2.Down * TileSize;
                _moveState += 1;
                break;
            }
            case 2:
            {
                Position += Vector2.Left * TileSize;
                _moveState += 1;
                break;
            }
            case 3:
            {
                Position += Vector2.Up * TileSize;
                _moveState += 1;
                break;
            }
            case 4:
            {
                Position += Vector2.Right * TileSize;
                _moveState = 1;
                break;
            }
        }
    }
}
