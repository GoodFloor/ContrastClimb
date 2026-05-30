using ContrastClimb.utils.enums;
using Godot;

namespace ContrastClimb.environment.platforms;

public partial class ParentPlatform : StaticBody2D
{
    [Export] 
    protected bool IsWhite;
    protected ParentPlatformSprite Sprite;

    private bool _isActive;

    public override void _Ready()
    {
        base._Ready();
        
        Sprite = GetNode<ParentPlatformSprite>("PlatformSprite");
        
        if (IsWhite)
        {
            _isActive = false;
            Sprite.HidePlatform();
            Sprite.MakeLight();
            
            CollisionLayer = (uint)ECollisionMask.WhiteDownwards;
        }
        else
        {
            _isActive = true;
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
        _isActive = !_isActive;
        
        if (_isActive)
            Sprite.RevealPlatform();
        else
            Sprite.HidePlatform();
    }

    public virtual void PlayerLanded()
    {
        // Default: Do nothing
    }
}
