using System;
using Godot;

public partial class MainCharacter : CharacterBody2D
{
    [Export]
    public int TileSize = 128;

    public override void _Process(double delta)
    {
        Vector2 direction = Vector2.Zero;

        if (Input.IsActionJustPressed("moveUp"))
            direction = Vector2.Up;
        else if (Input.IsActionJustPressed("moveDown"))
            direction = Vector2.Down;
        else if (Input.IsActionJustPressed("moveLeft"))
            direction = Vector2.Left;
        else if (Input.IsActionJustPressed("moveRight"))
            direction = Vector2.Right;

        if (direction != Vector2.Zero)
        {
            Position += direction * TileSize;
        }
    }
}
