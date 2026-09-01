using System;
using Godot;

public partial class MainCharacter : CharacterBody2D
{
    [Export]
    public int TileSize = 128;
    public double BeatInterval = 0.5; // seconds per beat
    public double Margin = 0.1; // strict window

    private double _startTime;

    public override void _Ready()
    {
        // Mark "beat zero" as the moment the level starts
        // Later: replace with Music playback position once the musci starts
        _startTime = Time.GetTicksMsec() / 1000.0;
    }

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
            double keypressTime = (Time.GetTicksMsec() / 1000.0) - _startTime;

            if (IsOnBeat(keypressTime))
            {
                Position += direction * TileSize;
            }
            else
            {
                GD.Print("YOU MISSED");
            }
        }
    }

    private bool IsOnBeat(double keypressTime)
    {
        int nearestIndex = (int)Math.Floor(keypressTime / BeatInterval);

        double currentBeat = nearestIndex * BeatInterval;
        double nextBeat = (nearestIndex + 1) * BeatInterval;

        bool early = Math.Abs(keypressTime - currentBeat) <= Margin;
        bool late = Math.Abs(keypressTime - nextBeat) <= Margin;

        return early || late;
    }
}
