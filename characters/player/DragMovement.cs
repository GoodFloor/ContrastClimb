namespace ContrastClimb.characters.player;

/// <summary>
/// Moving the player by mouse (without the need of clicking), or with touchscreen interaction.
/// </summary>
/// <param name="player">Target player to be moved.</param>
public partial class DragMovement(Player player) : Movement(player)
{
    protected override float GetVelocityX()
    {
        var directionVector = Player.GetViewport().GetMousePosition() - Player.GlobalPosition;
        return directionVector.X;
    }
}