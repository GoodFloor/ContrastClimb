using Godot;
using ContrastClimb.utils;
using ContrastClimb.utils.enums;

namespace ContrastClimb.characters.player;

public partial class Player : CharacterBody2D
{
    [Export] private bool _startsWhite;
    
    private const uint ColorSwitchMask = (uint)ECollisionMask.BlackAndWhite;
    private uint _currentColor = (uint)ECollisionMask.BlackAndSolid;
    
    private Movement _movementHandler;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("switch_color"))
        {
            _currentColor ^= ColorSwitchMask;
             
            if (_currentColor == (uint)ECollisionMask.BlackAndSolid)
            {
                Modulate = GColors.Dark;
            }
            else
            {
                Modulate = GColors.Light;
            }
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
            CollisionMask = _currentColor & (uint)ECollisionMask.DownwardsAndSolid;
        }
        else
        {
            CollisionMask = _currentColor & (uint)ECollisionMask.UpwardsAndSolid;
        }
        MoveAndSlide();
    }

    public override void _Ready()
    {
        base._Ready();
        
        Modulate = GColors.Dark;
        
        if (_startsWhite)
        {
            _currentColor ^= ColorSwitchMask;
            Modulate = GColors.Light;
        }
        CollisionMask = _currentColor;

        ReloadMovementConfig();
        
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
    }

    /// <summary>
    /// Checks the current config and loads the defined movement type. Should be called after editing the current config for the changes to take place. 
    /// </summary>
    private void ReloadMovementConfig()
    {
        _movementHandler = Global.Config.Steering switch
        {
            EMovementType.Tilt => new TiltMovement(this),
            EMovementType.Drag => new DragMovement(this),
            EMovementType.Keyboard => new KeyboardMovement(this),
            _ => null
        };
    }
}
