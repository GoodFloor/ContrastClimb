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
        Sprite = GetNode<ParentPlatformSprite>("PlatformSprite");
        
        if (IsWhite)
        {
            IsActive = false;
            Sprite.HidePlatform();
            Sprite.MakeLight();
            
            CollisionLayer = 2;
        }
        else
        {
            IsActive = true;
            Sprite.RevealPlatform();
            Sprite.MakeDark();
            
            CollisionLayer = 1;
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

    protected void SwitchColor()
    {
        IsActive = !IsActive;
        
        if (IsActive)
            Sprite.RevealPlatform();
        else
            Sprite.HidePlatform();
    }
}
