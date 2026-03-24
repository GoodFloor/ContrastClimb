using System;
using Godot;

namespace ContrastClimb.characters.player;

/// <summary>
/// Moving the player with the keyboard (i.e. A/D or arrow keys)
/// </summary>
/// <param name="player">Target player to be moved.</param>
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