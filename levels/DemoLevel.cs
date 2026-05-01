using Godot;
using System;

public partial class DemoLevel : Node2D
{
    private CameraPath _cameraPath;

    public override void _Ready()
    {
        base._Ready();
        _cameraPath = GetNode<CameraPath>("CameraPath");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float df = (float)delta;
    }

    public void StartCamera()
    {
        _cameraPath.StartCamera();
    }
}
