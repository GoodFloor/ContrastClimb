namespace ContrastClimb.characters.player;

public partial class DragMovement(Player player) : Movement(player)
{
    protected override float GetVelocityX()
    {
        var directionVector = Player.GetViewport().GetMousePosition() - Player.GlobalPosition;
        return directionVector.X;
    }
}