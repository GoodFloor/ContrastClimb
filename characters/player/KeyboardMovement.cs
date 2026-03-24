using System;
using Godot;

namespace ContrastClimb.characters.player;

public partial class KeyboardMovement(Player player) : Movement(player)
{
    protected override float GetVelocityX()
    {
        var v = 0f;
        if (Input.IsActionPressed("move_left"))
            v += -50;
        if (Input.IsActionPressed("move_right"))
            v += 50;
        return v;
    }
}