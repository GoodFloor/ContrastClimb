using Godot;
using System;

public partial class Platform : StaticBody2D
{
    [Export] 
    private bool _isWhite;

    public override void _Ready()
    {
        if (_isWhite)
            Visible = false;
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
