using Godot;

namespace ContrastClimb.characters.player;

/// <summary>
/// Moving the player by tilting the device. Can only be used on mobile devices.
/// </summary>
/// <param name="player">Target player to be moved.</param>
public partial class TiltMovement(Player player) : Movement(player)
{
    protected override float GetVelocityX()
    {
        return Input.GetGravity().X * 6f;
    }
}