using ContrastClimb.utils.enums;

namespace ContrastClimb.environment.platforms.always_solid;

public partial class SolidPlatform : ParentPlatform
{
    private bool _currentlyWhite;
    public override void _Ready()
    {
        base._Ready();

        CollisionLayer = (uint)ECollisionMask.AlwaysSolid;
        
        if (IsWhite)
            _currentlyWhite = true;
        
        if (_currentlyWhite)
            Sprite.MakeLight();
        else
            Sprite.MakeDark();
    }

    protected override void SwitchColor()
    {
        _currentlyWhite = !_currentlyWhite;
        
        if (_currentlyWhite)
            Sprite.MakeLight();
        else
            Sprite.MakeDark();
    }
}
