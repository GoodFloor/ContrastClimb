using Godot;
using System;

public partial class Platform : StaticBody2D
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("switch_color"))
        {
            GD.Print("switch_color");
            Visible = !Visible;
        }
    }
}
