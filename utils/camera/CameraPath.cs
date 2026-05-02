using Godot;
using ContrastClimb.characters.player;

namespace ContrastClimb.utils;

public partial class CameraPath : Path2D
{
    private PathFollow2D _pathFollow;
    
    private const float DefaultSpeed = 0.1f;
    private const float SpeedModifier = 0.1f;
    private float _speed = DefaultSpeed;
    public override void _Ready()
    {
        base._Ready();
        SetPhysicsProcess(false);
        _pathFollow = GetNode<PathFollow2D>("PathFollow");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float df = (float)delta;
        
        _pathFollow.ProgressRatio += df * _speed;
    }

    public void StartCamera()
    {
        SetPhysicsProcess(true);
    }

    private void OnBodyEnteredSpeedIncrease(Node2D body)
    {
        if (body is not Player)
            return;
        
        _speed += SpeedModifier;
    }

    private void OnBodyEnteredSpeedDecrease(Node2D body)
    {
        if (body is not Player)
            return;
        
        if (_speed > DefaultSpeed)
            _speed -= SpeedModifier;
    }
    
}