using System;
using Godot;

public partial class TrueSoul : AudioStreamPlayer
{
    [Export]
    public double BPM = 100.0;
    public double Margin = 0.1;
    public double StartOffset = 0.0; // To line up with the waveform MusicPlayer
    public double BeatInterval => 60.0 / BPM;

    [Signal]
    public delegate void BeatHitEventHandler(int beatIndex);

    public static TrueSoul Instance { get; private set; }
    private int _lastBeatIndex = -1;

    public override void _Ready()
    {
        Instance = this;
        Play();
    }

    public override void _Process(double delta)
    {
        double elapsed = GetElapsedTime();
        int currentBeatIndex = (int)Math.Floor(elapsed / BeatInterval);

        if (currentBeatIndex > _lastBeatIndex)
        {
            _lastBeatIndex = currentBeatIndex;
            GD.Print("Beat fired: " + currentBeatIndex);
            EmitSignal(SignalName.BeatHit, currentBeatIndex);
        }
    }

    public double GetElapsedTime()
    {
        return GetPlaybackPosition() - StartOffset;
    }

    public bool IsOnBeat(double keypressTime)
    {
        int nearestIndex = (int)Math.Floor(keypressTime / BeatInterval);

        double currentBeat = nearestIndex * BeatInterval;
        double nextBeat = (nearestIndex + 1) * BeatInterval;

        bool early = Math.Abs(keypressTime - currentBeat) <= Margin;
        bool late = Math.Abs(keypressTime - nextBeat) <= Margin;

        return early || late;
    }
}
