using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.environment.platforms.always_solid;

public partial class PlatformSprite : ParentPlatformSprite
{
    public override void HidePlatform()
    {
        // Do nothing
    }

    public override void RevealPlatform()
    {
        // Do nothing
    }

    public override void MakeLight()
    {
        GetNode<ColorRect>("ColorRect").Color = GColors.Light;
    }

    public override void MakeDark()
    {
        GetNode<ColorRect>("ColorRect").Color = GColors.Dark;
    }
}
