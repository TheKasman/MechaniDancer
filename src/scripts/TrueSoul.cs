using System;
using Godot;

public partial class TrueSoul : Node
{
    public static TrueSoul Instance { get; private set; }

    [Export]
    public double BPM = 120.0;
    public double Margin = 0.1;
    public double StartOffset = 0.0; // To line up with the waveform later

    public double BeatInterval => 60.0 / BPM;

    [Signal]
    public delegate void BeatHitEventHandler(int beatIndex);

    private double _startTime;
    private Timer _beatTimer;
    private int _beatCount = 0;

    public override void _Ready()
    {
        Instance = this;

        _startTime = Time.GetTicksMsec() / 1000.0;

        _beatTimer = new Timer();
        AddChild(_beatTimer);
        _beatTimer.WaitTime = BeatInterval;
        _beatTimer.OneShot = false; // REPEAT FOREVAAAA
        _beatTimer.Timeout += OnBeatTimerTimeout;
        _beatTimer.Start();
    }

    private void OnBeatTimerTimeout()
    {
        _beatCount++;
        EmitSignal(SignalName.BeatHit, _beatCount);
    }

    public double GetElapsedTime()
    {
        return (Time.GetTicksMsec() / 1000.0) - _startTime - StartOffset;
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
