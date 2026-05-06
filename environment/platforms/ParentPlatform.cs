using ContrastClimb.utils.enums;
using Godot;

namespace ContrastClimb.environment.platforms;

public partial class ParentPlatform : StaticBody2D
{
    [Export] 
    protected bool IsWhite;
    protected bool IsActive;
    protected ParentPlatformSprite Sprite;

    public override void _Ready()
    {
        base._Ready();
        
        Sprite = GetNode<ParentPlatformSprite>("PlatformSprite");
        
        if (IsWhite)
        {
            IsActive = false;
            Sprite.HidePlatform();
            Sprite.MakeLight();
            
            CollisionLayer = (uint)ECollisionMask.WhiteDownwards;
        }
        else
        {
            IsActive = true;
            Sprite.RevealPlatform();
            Sprite.MakeDark();
            
            CollisionLayer = (uint)ECollisionMask.BlackDownwards;
        }
        
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("switch_color"))
        {
            SwitchColor();
        }
    }

    protected virtual void SwitchColor()
    {
        IsActive = !IsActive;
        
        if (IsActive)
            Sprite.RevealPlatform();
        else
            Sprite.HidePlatform();
    }

    public virtual void PlayerLanded()
    {
        // Default: Do nothing
    }
}
