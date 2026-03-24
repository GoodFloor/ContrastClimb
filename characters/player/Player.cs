using Godot;
using ContrastClimb.characters.player;

public partial class Player : CharacterBody2D
{
    private const uint ColorSwitchMask = 1 + 2;     // This is a bit mask: 1 and 2 are black and white platforms, 4 are persistent platforms
    private uint _currentColor = 1 + 4;
    private Movement _movementHandler;

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
        var df = (float)delta;
        
        _movementHandler.HandleMovement(df);

        

        if (Velocity.Y >= 0f)
        {
            CollisionMask = _currentColor;
        }
        else
        {
            CollisionMask = 4;
        }
        MoveAndSlide();
    }

    public override void _Ready()
    {
        base._Ready();

        if (Global.Config.Steering == "tilt")
        {
            _movementHandler = new TiltMovement(this);
        }
        else
        {
            _movementHandler = new DragMovement(this);
        }
        
    }
}
