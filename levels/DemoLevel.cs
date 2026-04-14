using Godot;
using System;

public partial class DemoLevel : Node2D
{
    private Path2D _pathCamera;
    private PathFollow2D _pathFollow;
    private float _speed = 0.1f;
    private bool _isCameraStarted = false;

    public override void _Ready()
    {
        base._Ready();
        _pathCamera = GetNode<Path2D>("PathCamera");
        _pathFollow = _pathCamera.GetNode<PathFollow2D>("PathFollowCamera");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float df = (float)delta;

        if (_isCameraStarted)
            _pathFollow.ProgressRatio += df * _speed;
    }

    public void StartCamera()
    {
        _isCameraStarted = true;
    }
}
