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

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        float df = (float)delta;
        _velocity.Y = Velocity.Y;
        if (Input.IsActionPressed("move_right"))
        {
            _velocity.X = MovementSpeed;
        }
        else if (Input.IsActionPressed("move_left"))
        {
            _velocity.X = -MovementSpeed;
        }
        else
        {
            _velocity.X = 0f;
        }

        if (Input.IsActionJustPressed("jump") && _velocity.Y == 0f)
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
