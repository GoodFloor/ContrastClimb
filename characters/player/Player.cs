using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private Vector2 _velocity;
    private const float Gravity = 2048f;
    private const float JumpSpeed = 50000f;
    private const float MovementSpeed = 500f;
    private uint _currentColor = 1;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("switch_color"))
        {
            if (_currentColor == 1)
            {
                _currentColor = 2;
                CollisionMask = 2;
            }
            else
            {
                _currentColor = 1;
                CollisionMask = 1;
            }
            
        }
    }

    // Called once on each physics tick
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float df = (float)delta;
        _velocity.Y = Velocity.Y;
        
        GlobalPosition = new Vector2(GetViewport().GetMousePosition().X, GlobalPosition.Y);
        if (Input.GetGravity().X < 0f)
        {
            _velocity.X = MovementSpeed * Input.GetGravity().X / 5f;
        }
        else if (Input.GetGravity().X > 0f)
        {
            _velocity.X = MovementSpeed * Input.GetGravity().X / 5f;
        }
        else
        {
            _velocity.X = 0f;
        }

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
            CollisionMask = 0;
        }
        Velocity = _velocity;
        MoveAndSlide();
    }
}
