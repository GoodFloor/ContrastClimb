using Godot;

namespace ContrastClimb.characters.player;

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