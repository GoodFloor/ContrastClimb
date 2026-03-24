using Godot;
using ContrastClimb.characters.player;

public partial class Player : CharacterBody2D
{
    private Vector2 _velocity;
    private const float Gravity = 2048f;
    private const float JumpSpeed = 50000f;
    private const uint ColorSwitchMask = 1 + 2;     // This is a bit mask: 1 and 2 are black and white platforms, 4 are persistent platforms
    private uint _currentColor = 1 + 4;
    private HorizontalMovement _horizontalMovement;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("switch_color"))
        {
            CollisionMask ^= ColorSwitchMask;
            _currentColor ^= ColorSwitchMask;
        }
    }

    // Called once on each physics tick
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float df = (float)delta;
        _velocity.Y = Velocity.Y;
        _velocity.X = _horizontalMovement.GetSpeed(GlobalPosition);

        if (_velocity.Y == 0f)
        {
            _velocity.Y = df * -JumpSpeed;
        }
        else
        {
            _velocity.Y += df * Gravity;
        }

        if (_velocity.Y >= 0f)
        {
            CollisionMask = _currentColor;
        }
        else
        {
            CollisionMask = 4;
        }
        Velocity = _velocity;
        MoveAndSlide();
    }

    public override void _Ready()
    {
        base._Ready();

        if (Global.Config.Steering == "tilt")
        {
            _horizontalMovement = new TiltMovement();
        }
        else
        {
            _horizontalMovement = new DragMovement();
        }
        
    }
}
