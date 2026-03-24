using Godot;

namespace ContrastClimb.characters.player;

/// <summary>
/// Abstract class that handles the player movement. Subclasses of this class may change how the player can control the horizontal movement.  
/// </summary>
/// <param name="player">Target player to be moved.</param>
public abstract partial class Movement(Player player) : RefCounted
{
    private const float Gravity = 2048f;
    private const float JumpSpeed = 50000f;
    private const float MoveSpeed = 500f;
    protected Player Player = player;
    protected abstract float GetVelocityX();

    public void HandleMovement(float delta)
    {
        Vector2 velocity;
        
        // Vertical
        velocity.Y = Player.Velocity.Y;
        if (velocity.Y == 0f)
        {
            velocity.Y = delta * -JumpSpeed;
        }
        else
        {
            velocity.Y += delta * Gravity;
        }
        
        // Horizontal
        velocity.X = delta * GetVelocityX() * MoveSpeed;
        
        Player.Velocity = velocity;
    }
}