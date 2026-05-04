using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.environment.platforms.one_way;

public partial class PlatformSprite : ParentPlatformSprite
{
    public override void HidePlatform()
    {
        Visible = false;
    }

    public override void RevealPlatform()
    {
        Visible = true;
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
