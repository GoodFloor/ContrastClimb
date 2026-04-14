using Godot;
using System;

public partial class Background : ColorRect
{
    private bool _isWhite = true;
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Input.IsActionJustPressed("switch_color"))
        {
            if (_isWhite)
            {
                _isWhite = false;
                Color = Colors.Black;
            }
            else
            {
                _isWhite = true;
                Color = Colors.White;
            }
        }
    }
}
