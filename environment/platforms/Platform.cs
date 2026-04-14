using Godot;
using System;
using ContrastClimb.utils;

public partial class Platform : StaticBody2D
{
    [Export] 
    private bool _isWhite;

    public override void _Ready()
    {
        if (_isWhite)
        {
            Visible = false;
            CollisionLayer = 2;
            GetNode<ColorRect>("ColorRect").Color = GColors.Light;
        }
        else
        {
            GetNode<ColorRect>("ColorRect").Color = GColors.Dark;
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("switch_color"))
        {
            Visible = !Visible;
        }
    }
}
